using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShakeBehavior))]
public class ShakeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ShakeBehavior shakeScript = (ShakeBehavior)target;

        if (GUILayout.Button("Test Shake"))
        {
            // Trigger the shake using the current settings in the inspector
            shakeScript.Shake(shakeScript.ShakeDuration, shakeScript.ShakeIntensity);
        }
    }
}
