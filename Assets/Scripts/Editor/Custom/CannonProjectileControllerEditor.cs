using UnityEditor;
using UnityEngine;

namespace Cannon.Editor
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
