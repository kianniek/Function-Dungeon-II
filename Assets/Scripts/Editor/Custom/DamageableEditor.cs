using Targets;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    [CustomEditor(typeof(Damageable))]
    public class DamageableEditor : UnityEditor.Editor
    {
        private Damageable _script;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _script ??= (Damageable) target;

            if (GUILayout.Button("Test HP"))
            {
                _script.Health -= 1;
            }
        }
    }
}
