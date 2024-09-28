using UnityEditor;
using UnityEngine;

namespace EditorProgramming.E4.Editor
{
    public class PlayerEditor : EditorWindow
    {
        [MenuItem("Window/Test 3 Player Editor")]
        public static void ShowWindow()
        {
            GetWindow<PlayerEditor>("My Editor");
        }

        void OnGUI()
        {
            ExampleMethod();
        }

        void ExampleMethod()
        {
            var myIconSize = new Vector2(16, 16);
            using (new EditorGUIUtility.IconSizeScope(myIconSize))
            {
                if (GUILayout.Button(new GUIContent("My Button", "This is my button tooltip"), GUILayout.Width(100), GUILayout.Height(100)))
                {
                    Debug.Log("Button clicked!");
                }
            
                GUILayout.Label(new GUIContent("My Label with Icon", "This is a label tooltip"));
            }

            GUILayout.Label("This label will use the original icon size.");
        }
    }
}