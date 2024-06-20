using Kaijus;
using UnityEditor;

namespace Editor.Custom
{
    [CustomEditor(typeof(KaijuWaveManager))]
    public class KaijuWaveManagerEditor : UnityEditor.Editor
    {
        SerializedProperty kaijuPositionProp;
        SerializedProperty levelToPlayProp;
        SerializedProperty randomLevelProp;
        SerializedProperty kaijuPrefabsProp;
        SerializedProperty kaijusInLevelProp;
        SerializedProperty onKaijuDieProp;
        SerializedProperty kaijuSpawnProp;
        SerializedProperty completedLevelProp;

        private void OnEnable()
        {
            kaijuPositionProp = serializedObject.FindProperty("kaijuPosition");
            levelToPlayProp = serializedObject.FindProperty("levelToPlay");
            randomLevelProp = serializedObject.FindProperty("randomLevel");
            kaijuPrefabsProp = serializedObject.FindProperty("kaijuPrefabs");
            kaijusInLevelProp = serializedObject.FindProperty("kaijusInLevel");
            onKaijuDieProp = serializedObject.FindProperty("onKaijuDie");
            kaijuSpawnProp = serializedObject.FindProperty("KaijuSpawn");
            completedLevelProp = serializedObject.FindProperty("completedLevel");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(kaijuPositionProp);
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
            EditorGUILayout.PropertyField(kaijuSpawnProp);
            EditorGUILayout.PropertyField(completedLevelProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}