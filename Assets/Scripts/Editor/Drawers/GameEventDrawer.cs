using Editor.Custom;
using Events.GameEvents;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(GameEvent), true)]
    [CustomPropertyDrawer(typeof(GameEventBase<>), true)]
    public class GameEventDrawer : PropertyDrawer
    {
        private const int FoldoutWidth = 15;
        private const int HorizontalSpacing = 3;
        
        private UnityEditor.Editor _editorCache;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.objectReferenceValue is not null)
            {
                DrawGameEventProperty(position, property, label);
            }
            else
            {
                CreateDrawer.CreateDrawerExtension(position, property, label, fieldInfo);
            }
        }
        
        private void DrawGameEventProperty(Rect position, SerializedProperty property, GUIContent label)
        {
            if (_editorCache is null)
                UnityEditor.Editor.CreateCachedEditor(
                    property.objectReferenceValue,
                    typeof(UnityObjectEditor),
                    ref _editorCache
                );
            
            var targetObjectSerialized = _editorCache.serializedObject;
            var serializedPropertyDescription = targetObjectSerialized.FindProperty("description");
            
            // Create the rectangle for the foldout
            var foldoutRect = new Rect(position)
            {
                x = position.x - HorizontalSpacing + FoldoutWidth
            };
            
            // Draw the foldout triangle
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);
            
            var prefixLabelPosition = new Rect(position)
            {
                x = position.x + FoldoutWidth
            };
            
            if (!string.IsNullOrEmpty(serializedPropertyDescription.stringValue))
            {
                label.tooltip = serializedPropertyDescription.stringValue;
            }
            
            prefixLabelPosition = EditorGUI.PrefixLabel(prefixLabelPosition, label);
            
            var propertyPosition = new Rect(prefixLabelPosition)
            {
                x = prefixLabelPosition.x - FoldoutWidth
            };
            
            EditorGUI.PropertyField(propertyPosition, property, GUIContent.none);
            
            if (!property.isExpanded) 
                return;
            
            EditorGUI.indentLevel++;
            
            EditorGUI.BeginChangeCheck();
            
            EditorGUILayout.PropertyField(serializedPropertyDescription);
            
            if (!EditorGUI.EndChangeCheck()) 
                return;
            
            targetObjectSerialized.ApplyModifiedProperties();
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}