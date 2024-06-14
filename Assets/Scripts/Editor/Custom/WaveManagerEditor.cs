using UnityEditor;
using UnityEngine;
using WaveSystem;

namespace Editor.Custom
{
    [CustomEditor(typeof(WaveManager))]
    public class WaveManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default inspector
            
            var script = (WaveManager)target;
            
            if (GUILayout.Button("Spawn Wave(s)")) 
                script.SpawnWaves(); // Calls SpawnWave on the target object
        }
    }
}