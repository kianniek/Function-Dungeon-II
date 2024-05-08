using UnityEngine;
using UnityEditor;
namespace LineController
{
    [CustomEditor(typeof(FunctionLineController))]
    public class FunctionLineControllerEditor : Editor
    {
        private SerializedProperty isQuadratic;
        private SerializedProperty a;
        private SerializedProperty b;
        private SerializedProperty c;
        private SerializedProperty lineLength;
        private SerializedProperty segments;
        private SerializedProperty lineRenderer;

        private void OnEnable()
        {
            // Make sure these names exactly match the private fields in FunctionLineController
            isQuadratic = serializedObject.FindProperty("isQuadratic"); // Ensure this name matches
            a = serializedObject.FindProperty("a");
            b = serializedObject.FindProperty("b");
            c = serializedObject.FindProperty("c");
            lineLength = serializedObject.FindProperty("lineLength");
            segments = serializedObject.FindProperty("segments");
            lineRenderer = serializedObject.FindProperty("lineRenderer");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // Script field (non-editable)
            var script = MonoScript.FromMonoBehaviour((FunctionLineController)target);
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
            EditorGUI.EndDisabledGroup();

            // Toggle use of the exponent 'c'
            EditorGUILayout.PropertyField(isQuadratic, new GUIContent("Use Exponent (c in x^c)"));

            // Input fields for coefficients with labels
            EditorGUILayout.LabelField("Configure Linear Equation:");
            EditorGUILayout.PropertyField(a, new GUIContent("Coefficient a (Scale factor for x^c)"));
            EditorGUILayout.PropertyField(b, new GUIContent("Constant b (Vertical Shift)"));

            // Conditionally input field for 'c'
            if (isQuadratic.boolValue)
            {
                EditorGUILayout.PropertyField(c, new GUIContent("Exponent c (Power of x)"));
            }

            // Display the current equation with dynamic values
            var equation = "y = " + a.floatValue.ToString("F2") + "x";
            if (isQuadratic.boolValue && c.floatValue != 1)
            {
                equation += "^" + c.floatValue.ToString("F1");
            }
            equation += " + " + b.floatValue.ToString("F2");

            EditorGUILayout.LabelField("Current Equation: " + equation);

            // Additional settings
            EditorGUILayout.PropertyField(lineLength);
            EditorGUILayout.PropertyField(segments);
            EditorGUILayout.PropertyField(lineRenderer);

            serializedObject.ApplyModifiedProperties();
        }
    }
}