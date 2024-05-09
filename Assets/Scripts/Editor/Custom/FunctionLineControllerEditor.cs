using UnityEngine;
using UnityEditor;

namespace LineController.Editor
{
    //[CustomEditor(typeof(FunctionLineController))]
    public class FunctionLineControllerEditor : UnityEditor.Editor
    {
        private SerializedProperty _isQuadratic;
        private SerializedProperty _a;
        private SerializedProperty _b;
        private SerializedProperty _c;
        private SerializedProperty _lineLength;
        private SerializedProperty _segments;
        private SerializedProperty _lineRenderer;
        private SerializedProperty _debugProp;
        private SerializedProperty _projectileVelocityProp;
        private SerializedProperty _trajectoryLineRendererProp;
        private SerializedProperty _followDistanceProp;
        private SerializedProperty _gravityProp;
        private SerializedProperty _groundLevelProp;
        private SerializedProperty _timeStepProp;
        
        private void OnEnable()
        {
            // Make sure these names exactly match the private fields in FunctionLineController
            _isQuadratic = serializedObject.FindProperty("isQuadratic"); // Ensure this name matches
            _a = serializedObject.FindProperty("a");
            _b = serializedObject.FindProperty("b");
            _c = serializedObject.FindProperty("c");
            _lineLength = serializedObject.FindProperty("lineLength");
            _segments = serializedObject.FindProperty("segments");
            _lineRenderer = serializedObject.FindProperty("lineRenderer");
            
            _debugProp = serializedObject.FindProperty("debugMode");
            _projectileVelocityProp = serializedObject.FindProperty("projectileVelocity");
            _trajectoryLineRendererProp = serializedObject.FindProperty("trajectoryLineRenderer");
            _followDistanceProp = serializedObject.FindProperty("followDistance");
            _gravityProp = serializedObject.FindProperty("gravity");
            _groundLevelProp = serializedObject.FindProperty("groundLevel");
            _timeStepProp = serializedObject.FindProperty("timeStep");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            serializedObject.Update();
            
            FunctionLine();
            TrajectoryLine();
            
            serializedObject.ApplyModifiedProperties();
        }
        
        private void FunctionLine()
        {
            DisplayScriptField();
            DisplayQuadraticToggle();
            DisplayCoefficients();
            DisplayCurrentEquation();
            DisplayAdditionalSettings();
        }
        
        private void TrajectoryLine()
        {
            // DrawScriptField();
            DrawDebugProperties();
            DrawTrajectoryProperties();
            DrawEnvironmentalProperties();
            //DrawTrajectoryButton();
        }
        
        #region Trajectory Line
        
        // private void DrawScriptField()
        // {
        //     var script = MonoScript.FromMonoBehaviour((FunctionLineController)target);
        //     EditorGUI.BeginDisabledGroup(true);
        //     EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
        //     EditorGUI.EndDisabledGroup();
        // }
        
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
        }
        
        private void DrawEnvironmentalProperties()
        {
            EditorGUILayout.Space();
            // EditorGUILayout.PropertyField(_followDistanceProp, new GUIContent("Follow Distance"));
            // EditorGUILayout.PropertyField(_gravityProp, new GUIContent("Gravity"));
            EditorGUILayout.PropertyField(_groundLevelProp, new GUIContent("Ground Level"));
            EditorGUILayout.PropertyField(_timeStepProp, new GUIContent("Time Step"));
        }
        
        // private void DrawTrajectoryButton()
        // {
        //     if (GUILayout.Button("Draw Trajectory"))
        //     {
        //         ((FunctionLineController)target).CalculateTrajectory();
        //     }
        // }
        
        #endregion
        
        #region Function Line
        
        private void DisplayScriptField()
        {
            var script = MonoScript.FromMonoBehaviour((FunctionLineController)target);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();
        }
        
        private void DisplayQuadraticToggle()
        {
            EditorGUILayout.PropertyField(_isQuadratic, new GUIContent("Use Exponent (c in x^c)"));
        }
        
        private void DisplayCoefficients()
        {
            EditorGUILayout.LabelField("Configure Linear Equation:");
            EditorGUILayout.PropertyField(_a, new GUIContent("Coefficient a (Scale factor for x^c)"));
            EditorGUILayout.PropertyField(_b, new GUIContent("Constant b (Vertical Shift)"));
            
            if (_isQuadratic.boolValue)
            {
                EditorGUILayout.PropertyField(_c, new GUIContent("Exponent c (Power of x)"));
            }
        }
        
        private void DisplayCurrentEquation()
        {
            var equation = "y = " + _a.floatValue.ToString("F2") + "x";
            
            if (_isQuadratic.boolValue)
            {
                equation += "^" + _c.floatValue.ToString("F1");
            }
            
            equation += " + " + _b.floatValue.ToString("F2");
            EditorGUILayout.LabelField($"Current Equation: {equation}");
        }
        
        private void DisplayAdditionalSettings()
        {
            // EditorGUILayout.PropertyField(_lineLength);
            EditorGUILayout.PropertyField(_segments);
            EditorGUILayout.PropertyField(_lineRenderer);
        }
        
        #endregion
    }
}