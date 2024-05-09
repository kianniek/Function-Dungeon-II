using UnityEngine;
using UnityEditor;

namespace Shake.Editor
{
    [CustomEditor(typeof(ShakeBehavior))]
    public class ShakeBehaviorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var shakeScript = (ShakeBehavior)target;

            if (GUILayout.Button("Test Shake"))
            {
                // Trigger the shake using the current settings in the inspector
                shakeScript.Shake(shakeScript.DebugShakeDuration, shakeScript.DebugShakeIntensity);
            }
        }
    }
}