using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorProgramming.E1.Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        private Player _player;
        
        public bool showPosition = true;
        public string status = "Select a GameObject";
        
        public float knobValue = 0.5f;
        
        public string[] options = new string[] {"Cube", "Sphere", "Plane"};
        public int index = 0;
        
        public bool showBtn = true;
        
        AnimationCurve curveX = AnimationCurve.Linear(0, 0, 10, 10);

        public void OnEnable()
        {
            Debug.Log("On Enable is called");
            _player = (Player)target;
        }
        
        public void OnDisable()
        {
            Debug.Log("On Disable is called");
        }
        public void OnDestroy()
        {
            Debug.Log("On Destroy is called");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Custom Editor Inspector");
            EditorGUILayout.Space();
            EditorGUILayout.Separator();
            IntField();
            EditorGUILayout.Separator();
            Foldout();
            EditorGUILayout.Separator();
            Knob();
            EditorGUILayout.Separator();
            PopUp();
            EditorGUILayout.Separator();
            SliderMake();
            EditorGUILayout.Separator();
            ToggleMake();
            EditorGUILayout.Separator();
            HorizontalMaker();            
            EditorGUILayout.Separator();
            VerticalMaker();
            EditorGUILayout.Separator();
            EditorGUILayout.ColorField(Color.black);
            EditorGUILayout.Separator();
            EditorGUILayout.CurveField("Animation on X", curveX);
            EditorGUILayout.Separator();
            
            DrawDefaultInspector();
        }


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

        private void ToggleMake()
        {
            showBtn = EditorGUILayout.Toggle("Show Button", showBtn);
            if (showBtn)
                GUILayout.Button("Close");
            
        }

        private void SliderMake()
        {
            EditorGUILayout.LabelField("Slider");
            EditorGUILayout.Slider(50, 1, 100);
        }

        private void IntField()
        {
            EditorGUILayout.LabelField("Player Health");
            EditorGUILayout.IntField(_player.health);
        }
        private void PopUp()
        {
            EditorGUILayout.LabelField("Spawn Pop UpEditor Inspector");
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

        private void Foldout()
        {
            showPosition = EditorGUILayout.Foldout(showPosition, "Transform Position");
            
            if (!showPosition || Selection.activeTransform.IsUnityNull()) return;
            
            Selection.activeTransform.position =
                EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
            status = Selection.activeTransform.name;
            
            EditorGUILayout.LabelField("Current Value: " + knobValue.ToString("F2"));
        }
    }
}