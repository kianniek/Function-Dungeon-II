using UnityEngine;
using UnityEngine.Events;

namespace Shake
{
    /// <summary>
    /// Adds shake behavior to a GameObject in Unity. Can be configured to shake with or without decay.
    /// </summary>
    [ExecuteInEditMode]
    public class ShakeBehavior : MonoBehaviour
    {
        [SerializeField]
        UnityEvent OnShakeStart;
        [SerializeField]
        UnityEvent OnShakeEnd;
        [SerializeField]
        private float shakeDuration = 0.5f;

        /// <summary>
        /// Gets or sets the duration of the shake.
        /// </summary>
        public float ShakeDuration
        {
            get { return shakeDuration; }
            set { shakeDuration = value; }
        }

        [SerializeField]
        private Vector2 shakeIntensity = new Vector2(0.5f, 0.5f);

        /// <summary>
        /// Gets or sets the intensity of the shake on both axes.
        /// </summary>
        public Vector2 ShakeIntensity
        {
            get { return shakeIntensity; }
            set { shakeIntensity = value; }
        }

        [SerializeField]
        private bool enableDecay = true;

        /// <summary>
        /// Gets or sets a value indicating whether the shake intensity should decay over time.
        /// </summary>
        public bool EnableDecay
        {
            get { return enableDecay; }
            set { enableDecay = value; }
        }

        private Vector3 originalPosition;
        private float currentShakeDuration;
        private Vector2 initialShakeIntensity;

        /// <summary>
        /// Initializes shake parameters when the component awakes.
        /// </summary>
        private void Awake()
        {
            originalPosition = transform.localPosition;
        }

        /// <summary>
        /// Updates the shake behavior each frame, applying decay if enabled.
        /// </summary>
        private void Update()
        {
            if (currentShakeDuration > 0)
            {
                Vector3 shakeAmount;

                if (enableDecay)
                {
                    float decayRate = currentShakeDuration / shakeDuration;
                    shakeAmount = new Vector3(
                        initialShakeIntensity.x * Random.insideUnitSphere.x * decayRate,
                        initialShakeIntensity.y * Random.insideUnitSphere.y * decayRate,
                        0);
                }
                else
                {
                    shakeAmount = new Vector3(
                        initialShakeIntensity.x * Random.insideUnitSphere.x,
                        initialShakeIntensity.y * Random.insideUnitSphere.y,
                        0);
                }

                transform.localPosition = originalPosition + shakeAmount;
                currentShakeDuration -= Time.deltaTime;
                OnShakeStart.Invoke();
            }
            else
            {
                transform.localPosition = originalPosition;
                currentShakeDuration = 0;
                OnShakeEnd.Invoke();
            }
        }

        /// <summary>
        /// Starts a new shake with a specified duration and vector intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        /// <param name="intensity">Intensity of the shake on both axes.</param>
        public void Shake(float duration, Vector2 intensity)
        {
            currentShakeDuration = duration;
            initialShakeIntensity = intensity;
            shakeIntensity = intensity; // This can be removed if not used elsewhere
        }

        /// <summary>
        /// Starts a new shake with a specified duration and uniform intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        /// <param name="intensity">Uniform intensity of the shake on both axes.</param>
        public void Shake(float duration, float intensity)
        {
            Shake(duration, new Vector2(intensity, intensity));
        }

        /// <summary>
        /// Starts a new shake with the specified duration using the default intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        public void Shake(float duration)
        {
            Shake(duration, Vector2.one);
        }
    }
}