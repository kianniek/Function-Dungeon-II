using UnityEditor;
using UnityEngine;
using MineCart;

namespace Editor.Custom
{
    [CustomEditor(typeof(MineCartRailController))]
    public class MineCartRailControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // Draw the default inspector
            DrawDefaultInspector();
            
            // Get a reference to the MineCartRailController
            var controller = (MineCartRailController)target;
            
            // Add a button to the inspector
            if (GUILayout.Button("Invoke Track Completed"))
            {
                // Invoke the trackCompleted event
                controller.OnTrackCompleted();
            }
        }
    }
}