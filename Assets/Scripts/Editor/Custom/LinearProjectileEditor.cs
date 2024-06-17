using LinearProjectiles;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    /// <summary>
    /// Custom editor for the <see cref="LinearProjectile"/> class.
    /// </summary>
    [CustomEditor(typeof(LinearProjectile))]
    public class LinearProjectileEditor : UnityEditor.Editor
    {
        private const string SpeedTooltip =
            "Speed of the projectile";
        
        private const string DelayedGravityTooltip =
            "If true, gravity will be applied after a delay";
        
        private const string DelayTypeTooltip =
            "Type of delay before gravity is applied (time is in seconds)";
        
        private const string DelayValueTooltip =
            "Value of the delay before gravity is applied";
        
        private const string ResetOnInactivityTimeTooltip =
            "Deactivates the projectile after it has been inactive for a certain amount of seconds";
        
        private const string ResetOnDistanceTooltip =
            "Deactivates the projectile after it has traveled a certain distance";
        
        private const string ResetOnTimeTooltip =
            "Deactivates the projectile after a certain amount of seconds have passed since it was shot";
        
        private SerializedProperty _startPositionProp;
        private SerializedProperty _speedProp;
        private SerializedProperty _delayedGravityProp;
        private SerializedProperty _delayTypeProp;
        private SerializedProperty _delayValueProp;
        private SerializedProperty _resetOnInactivityTimeProp;
        private SerializedProperty _resetOnDistanceProp;
        private SerializedProperty _resetOnTimeProp;
        
        private void OnEnable()
        {
            _startPositionProp = serializedObject.FindProperty("startPosition");
            _speedProp = serializedObject.FindProperty("speed");
            _delayedGravityProp = serializedObject.FindProperty("delayedGravity");
            _delayTypeProp = serializedObject.FindProperty("delayType");
            _delayValueProp = serializedObject.FindProperty("delayValue");
            _resetOnInactivityTimeProp = serializedObject.FindProperty("resetOnInactivityTime");
            _resetOnDistanceProp = serializedObject.FindProperty("resetOnDistance");
            _resetOnTimeProp = serializedObject.FindProperty("resetOnTime");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            ProjectileSettings();
            PhysicsSettings();
            DeactivationSettings();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void DeactivationSettings()
        {
            EditorGUILayout.PropertyField(
                _resetOnInactivityTimeProp,
                new GUIContent(_resetOnInactivityTimeProp.displayName, ResetOnInactivityTimeTooltip)
            );
            
            EditorGUILayout.PropertyField(
                _resetOnDistanceProp,
                new GUIContent(_resetOnDistanceProp.displayName, ResetOnDistanceTooltip)
            );
            
            EditorGUILayout.PropertyField(
                _resetOnTimeProp,
                new GUIContent(_resetOnTimeProp.displayName, ResetOnTimeTooltip)
            );
        }
        
        private void ProjectileSettings()
        {
            EditorGUILayout.PropertyField(_startPositionProp);
        }
        
        private void PhysicsSettings()
        {
            EditorGUILayout.PropertyField(
                _speedProp,
                new GUIContent(_speedProp.displayName, SpeedTooltip)
            );
            
            EditorGUILayout.PropertyField(
                _delayedGravityProp,
                new GUIContent(_delayedGravityProp.displayName, DelayedGravityTooltip)
            );
            
            // If delayed gravity is not enabled, don't show the delay settings
            if (!_delayedGravityProp.boolValue) 
                return;
            
            EditorGUILayout.PropertyField(
                _delayTypeProp,
                new GUIContent(_delayTypeProp.displayName, DelayTypeTooltip)
            );
            
            EditorGUILayout.PropertyField(
                _delayValueProp,
                new GUIContent(_delayValueProp.displayName, DelayValueTooltip)
            );
        }
    }
}