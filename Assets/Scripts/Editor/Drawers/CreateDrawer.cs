using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Editor.Drawers
{
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class CreateDrawer : PropertyDrawer
    {
        private const int CreateButtonWidth = 66;
        private const int CreateButtonPadding = 2;
        private const string AssetPath = "Assets";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            CreateDrawerExtension(position, property, label, fieldInfo);
        }
        
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
                
                if (!GUI.Button(buttonRect, "Create")) return;
                
                property.objectReferenceValue = CreateAssetWithSavePrompt(label, GetFieldType(fieldInfo), AssetPath);
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
        
        // Creates a new ScriptableObject via the default Save File panel
        public static ScriptableObject CreateAssetWithSavePrompt(GUIContent label, Type type, string path)
        {
            path = EditorUtility.SaveFilePanelInProject(
                "Save ScriptableObject",
                label.text + ".asset",
                "asset",
                "Enter a file name for the ScriptableObject.",
                path);
            
            if (path == "") return null;
            
            var asset = ScriptableObject.CreateInstance(type);
            
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
            
            EditorGUIUtility.PingObject(asset);
            
            return asset;
        }
        
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