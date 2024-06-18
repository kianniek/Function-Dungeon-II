using Kaijus;
using UnityEditor;

namespace Editor.Custom
{
    [CustomEditor(typeof(KaijuWaveManager))]
    public class KaijuWaveManagerEditor : UnityEditor.Editor
    {
        SerializedProperty levelToPlayProp;
        SerializedProperty randomLevelProp;
        SerializedProperty kaijuPrefabsProp;
        SerializedProperty kaijusInLevelProp;
        SerializedProperty onKaijuDieProp;

        private void OnEnable()
        {
            levelToPlayProp = serializedObject.FindProperty("levelToPlay");
            randomLevelProp = serializedObject.FindProperty("randomLevel");
            kaijuPrefabsProp = serializedObject.FindProperty("kaijuPrefabs");
            kaijusInLevelProp = serializedObject.FindProperty("kaijusInLevel");
            onKaijuDieProp = serializedObject.FindProperty("onKaijuDie");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(randomLevelProp);

            if (randomLevelProp.boolValue)
            {
                EditorGUILayout.PropertyField(kaijuPrefabsProp, true);
                EditorGUILayout.PropertyField(kaijusInLevelProp);
            }
            else
            {
                EditorGUILayout.PropertyField(levelToPlayProp);
            }

            EditorGUILayout.PropertyField(onKaijuDieProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}