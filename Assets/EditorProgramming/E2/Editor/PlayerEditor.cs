using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.Graphs;
using UnityEngine.Events;

// Editor window example that demonstrates the use of EditorGUILayout.FadeGroupScope with animated foldouts.
// The window contains fields for various data types and shows or hides them based on a toggle.

namespace EditorProgramming.E2.Editor
{
    // Define a custom editor window called PlayerEditor
    public class PlayerEditor : EditorWindow
    {
        // AnimBool is used to manage the fade effect for the foldout group
        public AnimBool animBool;

        // Fields that will be toggled in the foldout
        public bool e1;            // Boolean field
        public string e2;          // String field
        public int e3;             // Integer field
        public Styles.Color e4 = Styles.Color.Blue;  // Enum field (custom enum defined elsewhere in your code)

        // Adds a menu item to the Unity editor for opening the PlayerEditor window
        [MenuItem("Window/Test 1 Player Editor")]
        static void Init()
        {
            // Create an instance of the PlayerEditor window and display it
            var windows = (PlayerEditor)EditorWindow.GetWindow(typeof(PlayerEditor));
            windows.Show();
        }

        // Called when the window is enabled/initialized
        void OnEnable()
        {
            // Initialize animBool with a default true value (fields will be shown initially)
            animBool = new AnimBool(true);
            
            // Add a listener to repaint the window when the AnimBool value changes
            animBool.valueChanged.AddListener(new UnityAction(base.Repaint));
        }
        
        // Called every time the GUI is drawn in the editor window
        private void OnGUI()
        {
            // Create a toggle button to show/hide additional fields
            animBool.target = EditorGUILayout.ToggleLeft("Show extra fields", animBool.target);

            // FadeGroupScope controls the visibility of the group of fields with an animation effect
            using var group = new EditorGUILayout.FadeGroupScope(animBool.faded);
            
            // If the group is visible (i.e., if the toggle is checked), display the additional fields
            if (group.visible)
            {
                EditorGUI.indentLevel++;  // Increase indentation for a cleaner layout
                
                // Display the fields in the editor window
                e1 = EditorGUILayout.Toggle("E1", e1);  // Boolean toggle for field e1
                e2 = EditorGUILayout.TextField("E2", e2);  // Text field for field e2
                e3 = EditorGUILayout.IntField("E3", e3);  // Integer field for field e3
                e4 = (Styles.Color)EditorGUILayout.EnumPopup("E4", e4);  // Enum popup for field e4
                
                EditorGUI.indentLevel--;  // Return indentation back to normal
            }
        }
    }
}
