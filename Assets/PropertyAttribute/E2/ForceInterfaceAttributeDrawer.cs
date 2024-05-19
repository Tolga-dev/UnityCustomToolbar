using UnityEditor;
using UnityEngine;

namespace PropertyAttribute.E2.Editor
{
    [CustomPropertyDrawer(typeof(ForceInterfaceAttribute))]
    public class ForceInterfaceAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ForceInterfaceAttribute forceAttribute = (ForceInterfaceAttribute)attribute;
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();
            Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, typeof(Object), !EditorUtility.IsPersistent(property.serializedObject.targetObject));
            if (EditorGUI.EndChangeCheck())
            {
                if (obj == null)
                {
                    property.objectReferenceValue = null;
                }
                else if (forceAttribute.InterfaceType.IsInstanceOfType(obj))
                {
                    property.objectReferenceValue = obj;
                }
                else if (obj is GameObject gameObject) 
                {
                    MonoBehaviour component = (MonoBehaviour)gameObject.GetComponent(forceAttribute.InterfaceType);
                    if (component != null)
                    {
                        property.objectReferenceValue = component;
                    }
                }
            }
            
            
        }
    }
}