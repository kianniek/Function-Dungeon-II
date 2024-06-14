using System.Globalization;
using Health;
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
            _script = (Damageable)target;
            
            if (EditorApplication.isPlaying)
            {
                GUILayout.Space(10);
                
                DebugBox();
                
                GUILayout.Space(10);
            }
            
            DrawDefaultInspector();
        }
        
        private void DebugBox()
        {
            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Debug", EditorStyles.boldLabel);
            
            HealthDebug();
            
            GUILayout.Space(5);
            
            EventsDebug();
            
            GUILayout.EndVertical();
        }
        
        private void EventsDebug()
        {
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Damage")) 
                _script.Health -= float.Epsilon;
            
            if (GUILayout.Button("Kill") && !_script.NegativeHealthEnabled) 
                _script.Health = 0;
            
            GUILayout.EndHorizontal();
        }
        
        private void HealthDebug()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Current Health");
            
            var previousGuiState = GUI.enabled;
            
            GUI.enabled = false;
            
            GUILayout.Label(_script.Health.ToString(CultureInfo.CurrentCulture), EditorStyles.textField);
            
            GUI.enabled = previousGuiState;
            
            GUILayout.EndHorizontal();
        }
    }
}