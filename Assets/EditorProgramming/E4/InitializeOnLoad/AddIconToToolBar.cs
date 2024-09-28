using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace EditorProgramming.E4.InitializeOnLoad
{

	[InitializeOnLoad]
	public class CreateButton
	{
		private static readonly Type ToolBar = typeof(Editor).Assembly.GetType("UnityEditor.Toolbar");
		private static ScriptableObject _currentToolbar;

		private static List<string> sceneList = new List<string>();
		private const float borderThickness = 2f;
		private static Rect rect;

		static CreateButton()
		{
			foreach (var scene in EditorBuildSettings.scenes)
			{
				sceneList.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path));
			}

			EditorApplication.update += OnUpdate;
		}

		private static void OnUpdate()
		{
			if (_currentToolbar == null)
			{
				_currentToolbar = PullToolBar();

				var rootField = _currentToolbar.GetType()
					.GetField("m_Root", BindingFlags.NonPublic | BindingFlags.Instance);
				var rawRoot = rootField?.GetValue(_currentToolbar);
				var mRoot = rawRoot as VisualElement;

				RegisterCallback("ToolbarZoneLeftAlign", OnToolbarGUILeft);

				void RegisterCallback(string root, Action cb)
				{
					var toolbarZone = mRoot.Q(root);
					var parent = new VisualElement()
					{
						style =
						{
							flexGrow = 1,
							flexDirection = FlexDirection.Row,
						}
					};
					var container = new IMGUIContainer();
					container.style.flexGrow = 1;
					container.onGUIHandler += () => { cb?.Invoke(); };
					parent.Add(container);
					toolbarZone.Add(parent);
				}

			}
		}

		private static ScriptableObject PullToolBar()
		{
			var toolBars = Resources.FindObjectsOfTypeAll(ToolBar);
			return toolBars.Length > 0 ? (ScriptableObject)toolBars[0] : null;
		}

		private static void OnToolbarGUILeft()
		{
			GUILayout.BeginHorizontal();

			for (int i = 0; i < sceneList.Count; i++)
			{
				var sceneName = sceneList[i];

				GUILayout.BeginHorizontal();

				if (GUILayout.Button(new GUIContent(sceneName, $"Log {sceneName}"), ToolbarStyles.commandButtonStyle, GUILayout.Width(20)))
				{
					Debug.Log(sceneName);
					SceneHelper.StartScene(sceneName);
				}

				if (GUILayout.Button(">", ToolbarStyles.commandButtonStyle, GUILayout.Width(20)))
				{
					Debug.Log(sceneName + " With Run");
					SceneHelper.StartSceneWithRun(sceneName);
				}

				if (GUILayout.Button("X", ToolbarStyles.commandButtonStyle, GUILayout.Width(20)))
				{
					sceneList.RemoveAt(i);
					Debug.Log($"{sceneName} removed from the list.");
					break; // Exit loop since the list has changed
				}

				GUILayout.EndHorizontal();
			}

			DrawDroppableArea();

			HandleDragAndDrop();

			GUILayout.EndHorizontal();
		}

		private static void DrawDroppableArea()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Box("Drag Scene Here", GUILayout.Width(150),
				GUILayout.Height(50)); // Fixed dimensions for droppable area
			GUILayout.EndHorizontal();
		}

		private static void HandleDragAndDrop()
		{
			Event evt = Event.current;

			if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
			{
				if (DragAndDrop.objectReferences.Length > 0)
				{
					for (int i = 0; i < DragAndDrop.objectReferences.Length; i++)
					{
						if (DragAndDrop.objectReferences[i] is SceneAsset)
						{
							DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
							break;
						}
					}
				}

				if (evt.type == EventType.DragPerform)
				{
					// Accept the drag and drop operation
					DragAndDrop.AcceptDrag();
					foreach (var obj in DragAndDrop.objectReferences)
					{
						if (obj is SceneAsset sceneAsset)
						{
							var sceneName =
								System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(sceneAsset));
							if (!sceneList.Contains(sceneName))
							{
								sceneList.Add(sceneName);
								Debug.Log($"{sceneName} added to the list.");
							}
						}
					}

					Event.current.Use();
				}
			}
		}
	}

	static class ToolbarStyles
	{
		public static readonly UnityEngine.GUIStyle commandButtonStyle;

		static ToolbarStyles()
		{
			commandButtonStyle = new UnityEngine.GUIStyle("Command")
			{
				fontSize = 10,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}

	static class SceneHelper
	{
		static string sceneToOpen;
		private static bool canRun = false;

		public static void StartScene(string sceneName)
		{
			canRun = false;
			OpenScene(sceneName);
		}

		public static void StartSceneWithRun(string sceneName)
		{
			canRun = true;
			OpenScene(sceneName);
		}

		private static void OpenScene(string sceneName)
		{
			if (EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			sceneToOpen = sceneName;
			EditorApplication.update += OnUpdate;
		}

		static void OnUpdate()
		{
			if (string.IsNullOrEmpty(sceneToOpen) ||
			    EditorApplication.isPlaying ||
			    EditorApplication.isPaused ||
			    EditorApplication.isCompiling ||
			    EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			EditorApplication.update -= OnUpdate;

			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
				if (guids.Length == 0)
				{
					Debug.LogWarning(
						$"Couldn't find scene file for '{sceneToOpen}'. Make sure it exists in your project.");
					return;
				}

				string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
				EditorSceneManager.OpenScene(scenePath);
				EditorApplication.isPlaying = canRun;
			}
			else
			{
				Debug.LogWarning("Scene not saved, canceling scene open.");
			}

			sceneToOpen = null;
		}
	}

}
