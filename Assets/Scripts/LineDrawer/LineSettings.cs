using UnityEngine;

namespace LineController
{
    [ExecuteInEditMode]
    public class LineSettings : MonoBehaviour
    {
        [SerializeField] private LineRenderer functionLine;
        [SerializeField] private LineRenderer trajectoryLine;

        [SerializeField] private LineSettingVariables functionLineSettings;
        [SerializeField] private LineSettingVariables trajectoryLineSettings;

        private void OnEnable()
        {
            SubscribeChanges();
        }

        private void OnDisable()
        {
            UnsubscribeChanges();
        }

        private void SubscribeChanges()
        {
            if (functionLineSettings != null)
                functionLineSettings.OnSettingsChanged += ApplyFunctionLineSettings;
            if (trajectoryLineSettings != null)
                trajectoryLineSettings.OnSettingsChanged += ApplyTrajectoryLineSettings;
        }

        private void UnsubscribeChanges()
        {
            if (functionLineSettings != null)
                functionLineSettings.OnSettingsChanged -= ApplyFunctionLineSettings;
            if (trajectoryLineSettings != null)
                trajectoryLineSettings.OnSettingsChanged -= ApplyTrajectoryLineSettings;
        }

        private void ApplyFunctionLineSettings()
        {
            ApplySettings(functionLine, functionLineSettings);
        }

        private void ApplyTrajectoryLineSettings()
        {
            ApplySettings(trajectoryLine, trajectoryLineSettings);
        }

        void ApplySettings(LineRenderer lineRenderer, LineSettingVariables settings)
        {
            if (lineRenderer != null && settings != null)
            {
                lineRenderer.alignment = settings.Alignment;
                lineRenderer.colorGradient = settings.ColorGradient;
                lineRenderer.widthCurve = settings.WidthCurve;
                lineRenderer.numCapVertices = settings.NumCapVertices;
                lineRenderer.numCornerVertices = settings.NumCornerVertices;
                lineRenderer.textureMode = settings.TextureMode;
                lineRenderer.material = settings.LineMaterial != null ? settings.LineMaterial : new Material(Shader.Find("Universal Render Pipeline/2D/Sprite-Lit-Default"));
                lineRenderer.widthMultiplier = settings.WidthMultiplier;
            }
        }
    }
}