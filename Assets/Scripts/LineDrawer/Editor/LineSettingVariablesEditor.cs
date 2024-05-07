using UnityEngine;
using UnityEditor;

namespace LineController
{
    [CustomEditor(typeof(LineSettingVariables))]
    public class LineSettingVariablesEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();

            // If any property is changed in the inspector, call this block
            if (GUI.changed)
            {
                var settings = (LineSettingVariables)target;
                settings.NotifyChanged();
            }
        }
    }
}