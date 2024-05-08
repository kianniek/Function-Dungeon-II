using UnityEngine;
using System;
using UnityEngine.Events;

namespace LineController
{
    [ExecuteInEditMode]
    [CreateAssetMenu(fileName = "LineSettingVariables", menuName = "LineDrawer/LineSettingVariables")]
    public class LineSettingVariables : ScriptableObject
    {
        public event UnityAction OnSettingsChanged;

        [SerializeField] private LineAlignment alignment = LineAlignment.View;
        public LineAlignment Alignment
        {
            get => alignment;
            set
            {
                if (alignment != value)
                {
                    alignment = value;
                    OnSettingsChanged?.Invoke();
                }
            }
        }

        [SerializeField] private Gradient colorGradient = new Gradient();
        public Gradient ColorGradient
        {
            get => colorGradient;
            set
            {
                colorGradient = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField] private Material lineMaterial;
        public Material LineMaterial
        {
            get => lineMaterial;
            set
            {
                lineMaterial = value;
                OnSettingsChanged?.Invoke();
            }
        }


        [SerializeField] private float textureScale = 1f;
        public float TextureScale
        {
            get => textureScale;
            set
            {
                textureScale = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField] private LineTextureMode textureMode = LineTextureMode.Stretch;
        public LineTextureMode TextureMode
        {
            get => textureMode;
            set
            {
                textureMode = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField] private AnimationCurve widthCurve = AnimationCurve.Constant(0f, 1f, 1f);
        public AnimationCurve WidthCurve
        {
            get => widthCurve;
            set
            {
                widthCurve = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField, Min(0f)] private float widthMultiplier = 1f;
        public float WidthMultiplier
        {
            get => widthMultiplier;
            set
            {
                widthMultiplier = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField, Min(0)] private int numCapVertices = 8;
        public int NumCapVertices
        {
            get => numCapVertices;
            set
            {
                numCapVertices = value;
                OnSettingsChanged?.Invoke();
            }
        }

        [SerializeField, Min(0)] private int numCornerVertices = 8;
        public int NumCornerVertices
        {
            get => numCornerVertices;
            set
            {
                numCornerVertices = value;
                OnSettingsChanged?.Invoke();
            }
        }

        public void OnValidate()
        {
            OnSettingsChanged?.Invoke();
        }
    }
}