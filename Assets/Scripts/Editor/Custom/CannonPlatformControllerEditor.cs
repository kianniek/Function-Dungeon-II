using Cannon;
using UnityEditor;
using UnityEngine;

namespace Editor.Custom
{
    [CustomEditor(typeof(LerpedYTranslation))]
    public class CannonPlatformControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector(); // Draws the default inspector

            var script = (LerpedYTranslation)target;

            if (GUILayout.Button("Move Up (1f)"))
            {
                script.MoveUp(1.0f); // Calls MoveUp on the target object
            }

            if (GUILayout.Button("Move Down (1f)"))
            {
                script.MoveDown(1.0f); // Calls MoveDown on the target object
            }
        }
    }
}
