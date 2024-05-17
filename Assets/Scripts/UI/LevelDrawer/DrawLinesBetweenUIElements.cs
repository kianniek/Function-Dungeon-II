using UnityEngine;
using UnityEngine.UI;

public class DrawUILinesBetweenElements : MonoBehaviour
{
    [SerializeField] private RectTransform parentUIElement; // The parent UI element containing the child elements
    [SerializeField] private GameObject linePrefab; // Prefab for the line (an Image with a simple line sprite)
    [SerializeField] private float lineWidth; // Width of the line
    [SerializeField] private Color lineColor; // Color of the line

    private RectTransform[] _childElements;
    private Image[] _lines;

    private const int MinAmountChildren = 2;

    private void Start()
    {
        var childCount = parentUIElement.childCount;

        if (childCount < MinAmountChildren)
        {
            Debug.LogWarning("Not enough child elements to draw lines");
            return;
        }

        _childElements = new RectTransform[childCount];
        _lines = new Image[childCount - 1];

        for (var i = 0; i < childCount; i++)
        {
            _childElements[i] = parentUIElement.GetChild(i) as RectTransform;

            if (i < childCount - 1)
            {
                var lineObj = Instantiate(linePrefab, parentUIElement);
                _lines[i] = lineObj.GetComponent<Image>();
            }
        }

        UpdateLinePositions();
    }

    private void UpdateLinePositions()
    {
        for (var i = 0; i < _lines.Length; i++)
        {
            var start = _childElements[i].anchoredPosition;
            var end = _childElements[i + 1].anchoredPosition;
            DrawLine(_lines[i], start, end);
        }
    }

    private void DrawLine(Image line, Vector2 start, Vector2 end)
    {
        var direction = end - start;
        var distance = direction.magnitude;

        line.rectTransform.sizeDelta = new (distance, lineWidth);
        line.rectTransform.pivot = new (0, 0.5f);
        line.rectTransform.anchoredPosition = start;
        line.color = lineColor;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        line.rectTransform.rotation = Quaternion.Euler(0, 0, angle);

        line.transform.SetSiblingIndex(0);
    }
}
