using UnityEditor;
using UnityEngine;

namespace UI.GraphDrawer.Editor
{
    [CustomEditor(typeof(GridDrawer))]
    public class GridDrawerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var gridDrawer = (GridDrawer)target;
            if (GUILayout.Button("Redraw Grid"))
            {
                gridDrawer.RedrawGrid();
            }
        }
    }
}