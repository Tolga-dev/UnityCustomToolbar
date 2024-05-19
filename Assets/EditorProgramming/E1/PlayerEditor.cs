using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorProgramming.E1.Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : UnityEditor.Editor
    {
        private Player _player;

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
            EditorGUILayout.LabelField("Player Health");
            EditorGUILayout.IntField(_player.health);
            DrawDefaultInspector();
        }
        
        public override VisualElement CreateInspectorGUI()
        {
            
            return base.CreateInspectorGUI();
        }
        
    }
}