using UnityEngine;
using static UnityEditorInternal.ReorderableList;

[System.Serializable]
class LineSettingsVariables
{
    public LineAlignment Alignment = LineAlignment.View;
    public Gradient ColorGradient = new Gradient();
    public Material lineMaterial;
    public float TextureScale = 1f;
    public LineTextureMode TextureMode = LineTextureMode.Stretch;
    public AnimationCurve WidthCurve = AnimationCurve.Constant(0f, 1f, 1f);
    [Min(0f)]
    public float WidthMultiplier = 1f;
    [Min(0f)]
    public int NumCapVertices = 8;
    [Min(0f)]
    public int NumCornerVertices = 8;
}

[ExecuteInEditMode]
public class LineSettings : MonoBehaviour
{
    [SerializeField] private LineRenderer functionLine;
    [SerializeField] private LineRenderer trajectoryLine;

    [SerializeField] private LineSettingsVariables functionLineSettings;
    [SerializeField] private LineSettingsVariables trajectoryLineSettings;

    private bool settingsChanged = true;

    void ApplySettings(LineRenderer lineRenderer, LineSettingsVariables settings)
    {
        if (lineRenderer != null && settings != null)
        {
            lineRenderer.alignment = settings.Alignment;
            lineRenderer.colorGradient = settings.ColorGradient;
            lineRenderer.widthCurve = settings.WidthCurve;
            lineRenderer.numCapVertices = settings.NumCapVertices;
            lineRenderer.numCornerVertices = settings.NumCornerVertices;
            lineRenderer.textureMode = settings.TextureMode;
            lineRenderer.textureScale = Vector2.one * settings.TextureScale;
            lineRenderer.widthMultiplier = settings.WidthMultiplier;

            Material Sprite_Lit_Default = new(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"))
            {
                name = "Line Default Material"
            };
            lineRenderer.material = settings.lineMaterial != null ? settings.lineMaterial : Sprite_Lit_Default;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (settingsChanged)
        {
            ApplySettings(functionLine, functionLineSettings);
            ApplySettings(trajectoryLine, trajectoryLineSettings);
            settingsChanged = false;
        }
    }

    // Call this method when any settings change
    public void SettingsHaveChanged()
    {
        settingsChanged = true;
    }

    private void OnValidate()
    {
        SettingsHaveChanged();
    }
}
