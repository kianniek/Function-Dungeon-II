using Kaijus;
using UnityEditor;

namespace Editor.Custom
{
    [CustomEditor(typeof(KaijuWaveManager))]
    public class KaijuWaveManagerEditor : UnityEditor.Editor
    {
        SerializedProperty waveToPlayProp;
        SerializedProperty randomWaveProp;
        SerializedProperty kaijuPrefabsProp;
        SerializedProperty kaijusInWaveProp;
        SerializedProperty onKaijuDieProp;

        private void OnEnable()
        {
            waveToPlayProp = serializedObject.FindProperty("waveToPlay");
            randomWaveProp = serializedObject.FindProperty("randomWave");
            kaijuPrefabsProp = serializedObject.FindProperty("kaijuPrefabs");
            kaijusInWaveProp = serializedObject.FindProperty("kaijusInWave");
            onKaijuDieProp = serializedObject.FindProperty("onKaijuDie");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(randomWaveProp);

            if (randomWaveProp.boolValue)
            {
                EditorGUILayout.PropertyField(kaijuPrefabsProp, true);
                EditorGUILayout.PropertyField(kaijusInWaveProp);
            }
            else
            {
                EditorGUILayout.PropertyField(waveToPlayProp);
            }

            EditorGUILayout.PropertyField(onKaijuDieProp);

            serializedObject.ApplyModifiedProperties();
        }
    }
}