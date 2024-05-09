using UnityEngine;
using UnityEditor;

namespace Targets.Editor
{
    [CustomEditor(typeof(HitScript))]
    public class CannonPlatformControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default inspector

            HitScript script = (HitScript)target;

            if (GUILayout.Button("Test HP"))
            {
                script.OnBlockHit(1); // Calls MoveUp on the target object
            }
        }
    }
}
