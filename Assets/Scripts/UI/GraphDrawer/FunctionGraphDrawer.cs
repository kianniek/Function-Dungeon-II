using UnityEngine;

namespace UI.GraphDrawer
{
    public class GridDrawer : MonoBehaviour
    {
        [SerializeField] private float lineThickness = 2f;
        [SerializeField] private float axisThickness = 4f;
        [SerializeField] private Color gridColor = Color.gray;
        [SerializeField] private Color axisColor = Color.black;
        [SerializeField] private LineRenderer lineRendererPrefabY;
        [SerializeField] private LineRenderer lineRendererPrefabX;
        [SerializeField] private int lineCount = 100;
        [SerializeField] private int step = 1;
        
        private void Start()
        {
            RedrawGrid();
        }

        public void RedrawGrid()
        {
            ClearGrid();
            DrawGrid();
        }

        private void ClearGrid()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        private void DrawGrid()
        {
            lineRendererPrefabY.startColor = axisColor;
            lineRendererPrefabY.endColor = axisColor;
            lineRendererPrefabX.startColor = axisColor;
            lineRendererPrefabX.endColor = axisColor;
            
            lineRendererPrefabX.startWidth = lineRendererPrefabX.endWidth = axisThickness;
            lineRendererPrefabY.startWidth = lineRendererPrefabY.endWidth = axisThickness;
            
            DrawGridLines();
        }
        
        private void DrawGridLines()
        {
            DrawHorizontalLines();
            DrawVerticalLines();
        }
        
        private void DrawHorizontalLines()
        {
            for (var i = 1; i <= lineCount; i++)
            {
                var line = CreateLine(lineRendererPrefabX);
                var lineTransform = line.GetComponent<RectTransform>();
                lineTransform.anchoredPosition = new Vector2(0, i * step);
                
                line = CreateLine(lineRendererPrefabX);
                lineTransform = line.GetComponent<RectTransform>();
                lineTransform.anchoredPosition = new Vector2(0, -i * step);
            }
        }
        
        private void DrawVerticalLines()
        {
            for (var i = 1; i <= lineCount; i++)
            {
                var line = CreateLine(lineRendererPrefabY);
                var lineTransform = line.GetComponent<RectTransform>();
                lineTransform.anchoredPosition = new Vector2(i * step, 0);
                
                line = CreateLine(lineRendererPrefabY);
                lineTransform = line.GetComponent<RectTransform>();
                lineTransform.anchoredPosition = new Vector2(-i * step, 0);
            }
        }
        
        private LineRenderer CreateLine(LineRenderer prefab)
        {
            var line = Instantiate(prefab, transform, true);
            
            line.startColor = line.endColor = gridColor;
            line.startWidth = line.endWidth = lineThickness;
            
            return line;
        }
    }
}
