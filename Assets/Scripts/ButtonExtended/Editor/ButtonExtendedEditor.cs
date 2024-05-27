using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace ButtonExtended.Editor
{
    [CustomEditor(typeof(ExtendedButton), true)]
    [CanEditMultipleObjects]
    public class ButtonExtendedEditor : UnityEditor.UI.ButtonEditor
    {
        SerializedProperty m_OnClickButtonProperty;
        SerializedProperty m_OnClickFloatProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OnClickButtonProperty = serializedObject.FindProperty("onClickButton");
            m_OnClickFloatProperty = serializedObject.FindProperty("onClickFloat");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_OnClickButtonProperty);
            EditorGUILayout.PropertyField(m_OnClickFloatProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
