using Attributes;
using Editor.Custom;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ExpandableAttribute), true)]
//    [CustomPropertyDrawer(typeof(SharedList<>), true)]
    public class ExpandableAttributeDrawer : PropertyDrawer
    {
        private const int FoldoutWidth = 15;
        private const int HorizontalSpacing = 3;
        
        // Cached scriptable object editor
        private UnityEditor.Editor _editor;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Make sure the property is valid
            if (property.objectReferenceValue is not null)
            {
                var labelPosition = new Rect(position)
                {
                    x = position.x + FoldoutWidth
                };
                
                labelPosition = EditorGUI.PrefixLabel(labelPosition, label);
                
                var propertyPosition = new Rect(labelPosition)
                {
                    x = labelPosition.x - FoldoutWidth
                };
                
                EditorGUI.PropertyField(propertyPosition, property, GUIContent.none);
                
                // Create the rectangle for the foldout
                var foldoutRect = new Rect(position)
                {
                    x = position.x - HorizontalSpacing + FoldoutWidth,
                    y = labelPosition.y
                };
                
                // Draw the foldout triangle
                property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);
                
                // Draw foldout properties
                if (!property.isExpanded)
                    return;
                
                // Cache the inspector data from the object referenced in the property
                if (_editor is null)
                    UnityEditor.Editor.CreateCachedEditor(
                        property.objectReferenceValue,
                        typeof(UnityObjectEditor),
                        ref _editor
                    );
                
                // Make child fields be indented
                EditorGUI.indentLevel++;
                
                EditorGUILayout.BeginVertical(EditorStyles.helpBox); // Start auto layout and Help-box style background
                
                // Start drawing objects after beginning change checking
                EditorGUI.BeginChangeCheck();
                
                if (_editor) // catch empty properties
                    _editor.OnInspectorGUI();
                
                // Check for changes, true when changes happened
                if (EditorGUI.EndChangeCheck())
                    property.serializedObject.ApplyModifiedProperties();
                
                EditorGUILayout.EndVertical();
                
                // Set indent back to what it was
                EditorGUI.indentLevel--;
            }
            else
            {
                CreateDrawer.CreateDrawerExtension(position, property, label, fieldInfo);
            }
        }
    }
}