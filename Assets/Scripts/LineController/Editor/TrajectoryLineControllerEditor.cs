using UnityEditor;
using UnityEngine;

namespace LineController.Editor
{
    [CustomEditor(typeof(TrajectoryLineController))]
    public class TrajectoryLineControllerEditor : UnityEditor.Editor
    {
        private SerializedProperty _debugProp;
        private SerializedProperty _projectileVelocityProp;
        private SerializedProperty _trajectoryLineRendererProp;
        private SerializedProperty _functionLineControllerProp;
        private SerializedProperty _followDistanceProp;
        private SerializedProperty _gravityProp;
        private SerializedProperty _groundLevelProp;
        private SerializedProperty _timeStepProp;

        private void OnEnable()
        {
            CacheSerializedProperties();
        }

        private void CacheSerializedProperties()
        {
            _debugProp = serializedObject.FindProperty("debugMode");
            _projectileVelocityProp = serializedObject.FindProperty("projectileVelocity");
            _trajectoryLineRendererProp = serializedObject.FindProperty("trajectoryLineRenderer");
            _functionLineControllerProp = serializedObject.FindProperty("functionLineController");
            _followDistanceProp = serializedObject.FindProperty("followDistance");
            _gravityProp = serializedObject.FindProperty("gravity");
            _groundLevelProp = serializedObject.FindProperty("groundLevel");
            _timeStepProp = serializedObject.FindProperty("timeStep");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawScriptField();
            DrawDebugProperties();
            DrawTrajectoryProperties();
            DrawEnvironmentalProperties();
            DrawTrajectoryButton();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawScriptField()
        {
            var script = MonoScript.FromMonoBehaviour((TrajectoryLineController)target);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }

        private void DrawDebugProperties()
        {
            EditorGUILayout.PropertyField(_debugProp, new GUIContent("Debug Mode"));
            if (_debugProp.boolValue)
            {
                EditorGUILayout.PropertyField(_projectileVelocityProp, new GUIContent("Projectile Velocity"));
            }
        }

        private void DrawTrajectoryProperties()
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_trajectoryLineRendererProp, new GUIContent("Trajectory Line Renderer"));
            EditorGUILayout.PropertyField(_functionLineControllerProp, new GUIContent("Function Line Controller"));
        }

        private void DrawEnvironmentalProperties()
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_followDistanceProp, new GUIContent("Follow Distance"));
            EditorGUILayout.PropertyField(_gravityProp, new GUIContent("Gravity"));
            EditorGUILayout.PropertyField(_groundLevelProp, new GUIContent("Ground Level"));
            EditorGUILayout.PropertyField(_timeStepProp, new GUIContent("Time Step"));
        }

        private void DrawTrajectoryButton()
        {
            if (GUILayout.Button("Draw Trajectory"))
            {
                ((TrajectoryLineController)target).CalculateTrajectory();
            }
        }
    }
}
