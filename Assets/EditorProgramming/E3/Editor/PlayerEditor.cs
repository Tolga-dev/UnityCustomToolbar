using UnityEditor;
using UnityEngine;
// https://docs.unity3d.com/ScriptReference/EditorGUILayout.ScrollViewScope.html
namespace EditorProgramming.E3.Editor
{
    public class PlayerEditor : EditorWindow
    {
        public Vector2 scrollPos;
        public string message = "Hello World";
        
        
        [MenuItem("Window/Test 2 Player Editor")]
        static void Init()
        {
            var window = GetWindow<PlayerEditor>();
            window.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            using var h = new EditorGUILayout.HorizontalScope("Button");
            
            if (GUI.Button(h.rect, GUIContent.none))
                Debug.Log("Go here");
            
            GUILayout.Label("A");
            GUILayout.Label("B");

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Separator();

            EditorGUILayout.BeginHorizontal();
            using var horizontalScope = new EditorGUILayout.HorizontalScope("Button");
            using var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos, GUILayout.Width(500), GUILayout.Height(500));
            
            scrollPos = scrollView.scrollPosition;
            GUILayout.Label(message);
            
            if (GUILayout.Button("Add More Text", GUILayout.Width(100), GUILayout.Height(100)))
                message += " \nAnd this is more text!";
            
            if (GUILayout.Button("Clear"))
                message = "";

            EditorGUILayout.EndHorizontal();
        }
        
    }
}
