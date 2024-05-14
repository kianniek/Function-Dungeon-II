using UnityEngine;
using UnityEngine.UI;

namespace UI.GraphDrawer
{
    public class GridDrawer : MonoBehaviour
    {
        [SerializeField] private float lineThickness = 2f;
        [SerializeField] private float axisThickness = 4f;
        [SerializeField] private Color gridColor = Color.gray;
        [SerializeField] private Color axisColor = Color.black;

        [SerializeField] private GameObject targetObject;
        [SerializeField] private Camera orthographicCamera;

        private RectTransform _rectTransform;
        private Canvas _canvas;

        void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();

            if (_rectTransform == null)
            {
                _rectTransform = gameObject.AddComponent<RectTransform>();
            }

            // Set the anchor points to stretch the entire parent
            _rectTransform.anchorMin = Vector2.zero;
            _rectTransform.anchorMax = Vector2.one;
            _rectTransform.sizeDelta = Vector2.zero;
        }

        void Start()
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

        void DrawGrid()
        {
            float unitSize = CalculateUnitSize();
            var width = _rectTransform.rect.width;
            var height = _rectTransform.rect.height;
            var targetPosition = Vector3.zero;

            if (targetObject != null)
            {
                targetPosition = Camera.main.WorldToScreenPoint(targetObject.transform.position);
            }

            // Draw Main X Axis
            DrawLine(new Vector2(0, targetPosition.y), new Vector2(width, targetPosition.y), axisThickness, axisColor);

            // Draw Main Y Axis
            DrawLine(new Vector2(targetPosition.x, 0), new Vector2(targetPosition.x, height), axisThickness, axisColor);

            // Draw Vertical Grid Lines
            for (var x = targetPosition.x; x <= width; x += unitSize)
            {
                DrawLine(new Vector2(x, 0), new Vector2(x, height), lineThickness, gridColor);
            }
            for (var x = targetPosition.x; x >= 0; x -= unitSize)
            {
                DrawLine(new Vector2(x, 0), new Vector2(x, height), lineThickness, gridColor);
            }

            // Draw Horizontal Grid Lines
            for (var y = targetPosition.y; y <= height; y += unitSize)
            {
                DrawLine(new Vector2(0, y), new Vector2(width, y), lineThickness, gridColor);
            }
            for (var y = targetPosition.y; y >= 0; y -= unitSize)
            {
                DrawLine(new Vector2(0, y), new Vector2(width, y), lineThickness, gridColor);
            }
        }

        float CalculateUnitSize()
        {
            if (orthographicCamera == null)
            {
                orthographicCamera = Camera.main;
            }

            float orthographicSize = orthographicCamera.orthographicSize;
            float screenHeight = Screen.height;
            float screenWidth = Screen.width;

            // The height of the orthographic camera in world units
            float worldHeight = orthographicSize * 2f;

            // The width of the orthographic camera in world units
            float worldWidth = (worldHeight / screenHeight) * screenWidth;

            // Calculate the unit size in pixels
            float unitSizeInPixels = screenHeight / worldHeight;

            return unitSizeInPixels;
        }

        void DrawLine(Vector2 start, Vector2 end, float thickness, Color color)
        {
            var line = new GameObject("Line");
            line.transform.SetParent(transform, false);

            var rt = line.AddComponent<RectTransform>();
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.sizeDelta = new Vector2(Vector2.Distance(start, end), thickness);

            rt.anchoredPosition = (start + end) / 2;
            rt.localRotation = Quaternion.Euler(0, 0, Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg);

            var image = line.AddComponent<Image>();
            image.color = color;
        }
    }
}
