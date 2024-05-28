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
            DrawDefaultInspector();

            var randomFloat = (RandomFloat)target;

            if (GUILayout.Button("Generate Random Float"))
            {
                randomFloat.GenerateRandomFloat();
            }
        }
    }

}