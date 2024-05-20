using Cannon;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    [CustomEditor(typeof(ProjectileLauncher))]
    public class ProjectileLauncherEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            ProjectileLauncher script = (ProjectileLauncher)target;
            
            if (GUILayout.Button("Test Shot"))
            {
                // Trigger the shot using the current settings in the inspector
                script.ShootProjectile();
            }
        }
    }
}
