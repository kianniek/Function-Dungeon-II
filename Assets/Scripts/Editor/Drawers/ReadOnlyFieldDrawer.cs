using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyFieldAttribute))]
    public class ReadOnlyFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var previousGUIState = GUI.enabled;
            
            GUI.enabled = false;
            
            EditorGUI.PropertyField(position, property, label);
            
            GUI.enabled = previousGUIState;
        }
    }
}