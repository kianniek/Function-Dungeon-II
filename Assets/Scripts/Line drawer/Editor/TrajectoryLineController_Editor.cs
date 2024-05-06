using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TrajectoryLineController))]
public class TrajectoryLineControllerEditor : Editor
{
    SerializedProperty debugProp;
    SerializedProperty projectileVelocityProp;
    SerializedProperty trajectoryLineRendererProp;
    SerializedProperty functionLineControllerProp;
    SerializedProperty followDistanceProp;
    SerializedProperty gravityProp;
    SerializedProperty groundLevelProp;
    SerializedProperty timeStepProp;

    void OnEnable()
    {
        // Cache the serialized properties
        debugProp = serializedObject.FindProperty("debugMode");  // Ensure these property names exactly match those in the TrajectoryLineController script
        projectileVelocityProp = serializedObject.FindProperty("projectileVelocity");
        trajectoryLineRendererProp = serializedObject.FindProperty("trajectoryLineRenderer");
        functionLineControllerProp = serializedObject.FindProperty("functionLineController");
        followDistanceProp = serializedObject.FindProperty("followDistance");
        gravityProp = serializedObject.FindProperty("gravity");
        groundLevelProp = serializedObject.FindProperty("groundLevel");
        timeStepProp = serializedObject.FindProperty("timeStep");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();  // Load the actual values

        // Script field
        MonoScript script = MonoScript.FromMonoBehaviour((TrajectoryLineController)target);
        EditorGUI.BeginDisabledGroup(true);
        EditorGUILayout.ObjectField("Script", script, typeof(MonoScript), false);
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.PropertyField(debugProp, new GUIContent("Debug Mode"));

        if (debugProp.boolValue)
        {
            // Show properties under the "Projectile Debug Settings" header
            EditorGUILayout.PropertyField(projectileVelocityProp, new GUIContent("Projectile Velocity"));
        }

        // Always visible properties
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(trajectoryLineRendererProp, new GUIContent("Trajectory Line Renderer"));
        EditorGUILayout.PropertyField(functionLineControllerProp, new GUIContent("Function Line Controller"));

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(followDistanceProp, new GUIContent("Follow Distance"));
        EditorGUILayout.PropertyField(gravityProp, new GUIContent("Gravity"));
        EditorGUILayout.PropertyField(groundLevelProp, new GUIContent("Ground Level"));
        EditorGUILayout.PropertyField(timeStepProp, new GUIContent("Time Step"));

        if (GUILayout.Button("Draw Trajectory"))
        {
            ((TrajectoryLineController)target).DrawTrajectory();
        }

        serializedObject.ApplyModifiedProperties();  // Save the modified values back
    }
}
