using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace EditorProgramming.E1.Editor
{
    // Enum definition to demonstrate custom options in the inspector
    public enum MyEnum
    {
        None = 0, // Custom name for "Nothing" option
        A = 1,
        B = 2,
        All = ~0, // Custom name for "Everything" option
    }

    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        // Variables to store editor states and values
        private Player _player;

        public bool showPosition = true; // To control foldout visibility
        public string status = "Select a GameObject";

        public float knobValue = 0.5f; // Value for a custom knob control

        // Dropdown options and selection index
        public string[] options = new string[] {"Cube", "Sphere", "Plane"};
        public int index = 0;

        public bool showBtn = true; // Toggle to show or hide a button

        AnimationCurve curveX = AnimationCurve.Linear(0, 0, 10, 10); // Example curve field

        // Various field values for delayed and standard field types
        public double dValue = 0.0f;
        public float fValue = 0.0f;
        public int iValue = 0;
        public string tValue = "Hello";

        // Toolbar settings
        public int toolbarInt = 0;
        public string[] toolbarStrings = {"Toolbar1", "Toolbar2", "Toolbar3"};

        // Enum fields for demonstration
        public MyEnum myEnum = MyEnum.A;
        public MyEnum myEnum2 = MyEnum.A;

        // Example float field
        public float multiPlayer = 1.0f;

        // Called when the custom editor is enabled
        public void OnEnable()
        {
            Debug.Log("On Enable is called");
            _player = (Player)target; // Cast target to Player
        }

        // Called when the custom editor is disabled
        public void OnDisable()
        {
            Debug.Log("On Disable is called");
        }

        // Called when the custom editor is destroyed
        public void OnDestroy()
        {
            Debug.Log("On Destroy is called");
        }

        // Override the default inspector GUI to add custom controls
        public override void OnInspectorGUI()
        {
            // Label and spacing to organize the inspector
            EditorGUILayout.LabelField("Custom Editor Inspector");
            EditorGUILayout.Space();
            EditorGUILayout.Separator();

            // Various custom methods to render different controls
            IntField(); // Display player health
            EditorGUILayout.Separator();
            Foldout(); // Show a foldout with transform position
            EditorGUILayout.Separator();
            Knob(); // Display a custom knob control
            EditorGUILayout.Separator();
            PopUp(); // Dropdown selection for primitives
            EditorGUILayout.Separator();
            SliderMake(); // Slider example
            EditorGUILayout.Separator();
            ToggleMake(); // Toggle to show/hide button
            EditorGUILayout.Separator();
            HorizontalMaker(); // Horizontal layout example
            EditorGUILayout.Separator();
            VerticalMaker(); // Vertical layout example
            EditorGUILayout.Separator();

            // Color field and animation curve example
            EditorGUILayout.ColorField(Color.black);
            EditorGUILayout.Separator();
            EditorGUILayout.CurveField("Animation on X", curveX);
            EditorGUILayout.Separator();

            // Delayed fields to input various data types
            EditorGUILayout.DelayedDoubleField("A value", dValue);
            EditorGUILayout.DoubleField("A value", dValue);
            EditorGUILayout.DelayedFloatField("A value", fValue);
            EditorGUILayout.DelayedIntField("A value", iValue);
            EditorGUILayout.DelayedTextField("A value", tValue);

            EditorGUILayout.Separator();

            // Dropdown button to display options
            DropDownMaker();
            EditorGUILayout.Separator();

            // Toolbar example
            toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
            EditorGUILayout.Separator();

            // Build target selection example
            EndEditorMaker();
            EditorGUILayout.Separator();

            // Enum fields to select from MyEnum options
            myEnum = (MyEnum)EditorGUILayout.EnumFlagsField(myEnum);
            EditorGUILayout.Separator();
            myEnum2 = (MyEnum)EditorGUILayout.EnumPopup("Primitive", myEnum2);

            // Button to log enum value
            if (GUILayout.Button("Click"))
                Debug.Log(myEnum2);

            EditorGUILayout.Separator();

            // Simple float field
            multiPlayer = EditorGUILayout.FloatField("INCREASE ME", multiPlayer);
            EditorGUILayout.Separator();

            // Custom label using GetControlRect
            Rect rect = EditorGUILayout.GetControlRect(false, 40); // Set height to 40
            GUI.Label(rect, "Custom Label in Control Rect");

            EditorGUILayout.Separator();

            // Help box to display information
            EditorGUILayout.HelpBox("This is a help box", MessageType.Info);
            EditorGUILayout.Separator();

            // Draw default inspector below custom controls
            DrawDefaultInspector();
        }

        // Method to demonstrate build target specific options
        private void EndEditorMaker()
        {
            BuildTargetGroup selectedBuildTargetGroup = EditorGUILayout.BeginBuildTargetSelectionGrouping();
            if (selectedBuildTargetGroup == BuildTargetGroup.Android)
            {
                EditorGUILayout.LabelField("Android specific things");
            }

            if (selectedBuildTargetGroup == BuildTargetGroup.Standalone)
            {
                EditorGUILayout.LabelField("Standalone specific things");
            }

            EditorGUILayout.EndBuildTargetSelectionGrouping();
        }

        // Method to display dropdown button and handle selections
        private void DropDownMaker()
        {
            if (EditorGUILayout.DropdownButton(new GUIContent("Options"), FocusType.Keyboard))
            {
                GenericMenu menu = new GenericMenu();

                // Add options to the dropdown menu
                menu.AddItem(new GUIContent("Option 1"), false, OnOptionSelected, "Option 1");
                menu.AddItem(new GUIContent("Option 2"), false, OnOptionSelected, "Option 2");
                menu.AddItem(new GUIContent("Option 3"), false, OnOptionSelected, "Option 3");

                menu.ShowAsContext();
            }
        }

        // Callback when an option from the dropdown is selected
        private void OnOptionSelected(object option)
        {
            Debug.Log("Selected: " + option);
        }

        // Method to create a vertical button layout
        private void VerticalMaker()
        {
            Rect r = EditorGUILayout.BeginVertical("Button");
            if (GUI.Button(r, GUIContent.none))
                Debug.Log("Go here");
            GUILayout.Label("A");
            GUILayout.Label("B");
            GUILayout.Label("C");
            GUILayout.Label("D");
            EditorGUILayout.EndHorizontal();
        }

        // Method to create a horizontal button layout
        private void HorizontalMaker()
        {
            Rect r = EditorGUILayout.BeginHorizontal("Button");
            if (GUI.Button(r, GUIContent.none))
                Debug.Log("Go here");
            GUILayout.Label("A");
            GUILayout.Label("B");
            GUILayout.Label("C");
            GUILayout.Label("D");
            EditorGUILayout.EndHorizontal();
        }

        // Method to display a toggle and conditionally show a button
        private void ToggleMake()
        {
            showBtn = EditorGUILayout.Toggle("Show Button", showBtn);
            if (showBtn)
                GUILayout.Button("Close");
        }

        // Method to display a slider
        private void SliderMake()
        {
            EditorGUILayout.LabelField("Slider");
            EditorGUILayout.Slider(50, 1, 100);
        }

        // Method to display an int field with player health
        private void IntField()
        {
            EditorGUILayout.LabelField("Player Health");
            EditorGUILayout.IntField(_player.health);
        }

        // Method to create a popup menu for spawning objects
        private void PopUp()
        {
            EditorGUILayout.LabelField("Spawn Pop Up Editor Inspector");
            index = EditorGUILayout.Popup(index, options);
            if (GUILayout.Button("Create"))
            {
                switch (index)
                {
                    case 0:
                        GameObject.CreatePrimitive(PrimitiveType.Cube);
                        break;
                    case 1:
                        GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        break;
                    case 2:
                        GameObject.CreatePrimitive(PrimitiveType.Plane);
                        break;
                }
            }
        }

        // Method to create a custom knob control
        private void Knob()
        {
            EditorGUILayout.LabelField("Custom Knob Example");
            EditorGUILayout.Space();

            knobValue = EditorGUILayout.Knob(
                new Vector2(50, 50), // Size of the knob
                knobValue,           // Current value
                0,                   // Minimum value
                1,                   // Maximum value
                "Value",             // Label
                Color.gray,          // Background color
                Color.green,         // Active color
                true                 // Show the value
            );
        }

        // Method to create a foldout for transform position
        private void Foldout()
        {
            showPosition = EditorGUILayout.Foldout(showPosition, "Transform Position");

            // If the foldout is expanded and a transform is selected, show its position
            if (!showPosition || Selection.activeTransform.IsUnityNull()) return;

            Selection.activeTransform.position =
                EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
            status = Selection.activeTransform.name;

            EditorGUILayout.LabelField("Current Value: " + knobValue.ToString("F2"));
        }
    }
}
