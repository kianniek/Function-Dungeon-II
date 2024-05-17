using Targets;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    [CustomEditor(typeof(HitScript))]
    public class HitScriptEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default inspector

            var script = (HitScript)target;

            if (GUILayout.Button("Test HP"))
            {
                script.OnBlockHit(1); // Calls MoveUp on the target object
            }
        }
    }
}
