using Cannon;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(CannonProjectileController))]
public class ShakeBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CannonProjectileController cannonScript = (CannonProjectileController)target;

        if (GUILayout.Button("Test Shot"))
        {
            // Trigger the shot using the current settings in the inspector
            cannonScript.ShootProjectile();
        }
    }
}
