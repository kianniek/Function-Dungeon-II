using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TrajectoryLineController))]
public class TrajectoryLineController_Editor : Editor
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
        debugProp = serializedObject.FindProperty("_debug");
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

        EditorGUILayout.PropertyField(debugProp);

        if (debugProp.boolValue)
        {
            // Show properties under the "Projectile Debug Settings" header
            EditorGUILayout.PropertyField(projectileVelocityProp);
        }

        // Always visible properties
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(trajectoryLineRendererProp);
        EditorGUILayout.PropertyField(functionLineControllerProp);

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(followDistanceProp);
        EditorGUILayout.PropertyField(gravityProp);
        EditorGUILayout.PropertyField(groundLevelProp);
        EditorGUILayout.PropertyField(timeStepProp);

        if (debugProp.boolValue)
        {
            if (GUILayout.Button("Update Trajectory"))
            {
                ((TrajectoryLineController)target).DrawTrajectory();
            }
        }
        

        serializedObject.ApplyModifiedProperties();  // Save the modified values back
    }
}