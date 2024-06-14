using System;
using System.Collections.Generic;
using System.Reflection;
using Attributes;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    /// <summary>
    /// Custom property drawer for creating ScriptableObjects.
    /// </summary>
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class CreateDrawer : PropertyDrawer
    {
        private const int CreateButtonWidth = 66;
        private const int CreateButtonPadding = 2;
        private const string AssetPath = "Assets";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.GetCustomAttribute<ExpandableAttribute>() is null)
            {
                CreateDrawerExtension(position, property, label, fieldInfo);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
        
        /// <summary>
        /// Extension of the OnGUI method to allow for the creation of ScriptableObjects.
        /// </summary>
        /// <param name="position"> The position of the property. </param>
        /// <param name="property"> The property to draw. </param>
        /// <param name="label"> The label of the property. </param>
        /// <param name="fieldInfo"> The field information of the property. </param>
        public static void CreateDrawerExtension(Rect position, SerializedProperty property, GUIContent label,
            FieldInfo fieldInfo)
        {
            if (property.propertyType is SerializedPropertyType.ObjectReference &&
                property.objectReferenceValue is null)
            {
                var fieldRect = new Rect(position)
                {
                    width = position.width - CreateButtonWidth - CreateButtonPadding,
                    height = EditorGUIUtility.singleLineHeight
                };
                
                EditorGUI.PropertyField(fieldRect, property, label, true);
                
                var buttonRect = new Rect(fieldRect)
                {
                    x = fieldRect.x + fieldRect.width + CreateButtonPadding,
                    width = CreateButtonWidth
                };
                
                if (!GUI.Button(buttonRect, "Create"))
                    return;
                
                property.objectReferenceValue = CreateAssetWithSavePrompt(label, GetFieldType(fieldInfo), AssetPath);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
        
        /// <summary>
        /// Creates a new ScriptableObject via the default Save File panel.
        /// </summary>
        /// <param name="label"> The label of the ScriptableObject. </param>
        /// <param name="type"> The type of the ScriptableObject. </param>
        /// <param name="path"> The path to save the ScriptableObject. </param>
        /// <returns></returns>
        public static ScriptableObject CreateAssetWithSavePrompt(GUIContent label, Type type, string path)
        {
            path = EditorUtility.SaveFilePanelInProject(
                "Save ScriptableObject",
                label.text + ".asset",
                "asset",
                "Enter a file name for the ScriptableObject.",
                path);
            
            if (path is "")
                return null;
            
            var asset = ScriptableObject.CreateInstance(type);
            
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            
            EditorGUIUtility.PingObject(asset);
            
            return asset;
        }
        
        /// <summary>
        /// Get the type of the field.
        /// </summary>
        /// <param name="fieldInfo"> The field to get the type of. </param>
        /// <returns> The type of the field. </returns>
        public static Type GetFieldType(FieldInfo fieldInfo)
        {
            var type = fieldInfo.FieldType;
            
            if (type.IsArray)
                type = type.GetElementType();
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                type = type.GetGenericArguments()[0];
            
            return type;
        }
    }
}