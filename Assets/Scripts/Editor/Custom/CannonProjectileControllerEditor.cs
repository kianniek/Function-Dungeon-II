using Cannon;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    [CustomEditor(typeof(CannonProjectileController))]
    public class CannonProjectileControllerEditor : UnityEditor.Editor
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
}
