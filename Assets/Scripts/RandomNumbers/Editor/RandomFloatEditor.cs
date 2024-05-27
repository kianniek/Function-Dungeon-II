using UnityEngine;
using UnityEditor;

namespace RandomNumbers
{
    [CustomEditor(typeof(RandomFloat))]
    [CanEditMultipleObjects]
    public class RandomFloatEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // Get the RandomFloat script instance
            RandomFloat randomFloat = (RandomFloat)target;

            // Add a button to the inspector
            if (GUILayout.Button("Generate Random Float"))
            {
                // Call the GenerateRandomFloat function
                randomFloat.GenerateRandomFloat();
            }
        }
    }

}