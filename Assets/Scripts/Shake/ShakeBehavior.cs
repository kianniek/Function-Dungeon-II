using System.Collections;
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
        [Header("Debugging")]
        [Tooltip("This variable is only used when hitting the <b>Test Shake</b> button in the inspector")]
        [SerializeField] private Vector2 debugShakeIntensity = new(0.5f, 0.5f);
        [Tooltip("This variable is only used when hitting the <b>Test Shake</b> button in the inspector")]
        [SerializeField] private float debugShakeDuration = 0.5f;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onShakeStart = new(); 
        [SerializeField] private UnityEvent onShakeEnd = new();
                
        private Vector3 _originalPosition;

        public float DebugShakeDuration => debugShakeDuration;
        
        public Vector2 DebugShakeIntensity => debugShakeIntensity;
        
        public bool EnableDecay => true;
        
        /// <summary>
        /// Initializes shake parameters when the component awakes.
        /// </summary>
        private void Start()
        {
            _originalPosition = transform.localPosition;
        }

        /// <summary>
        /// Coroutine that performs the shake.
        /// </summary>
        /// <param name="duration">Duration of the shake in seconds.</param>
        /// <param name="intensity">Intensity of the shake on both axes.</param>
        /// <returns></returns>
        private IEnumerator ShakeCoroutine(float duration, Vector2 intensity)
        {
            onShakeStart.Invoke();
            //reset the position of the object before shaking
            transform.localPosition = _originalPosition;
            var elapsed = 0.0f;

            //Debug.Log("Help me! I'm shaking! " + duration + " " + intensity);
            
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var decayRate = EnableDecay ? (1 - (elapsed / duration)) : 1;
                var shakeAmount = new Vector3(
                    intensity.x * Random.insideUnitSphere.x * decayRate,
                    intensity.y * Random.insideUnitSphere.y * decayRate,
                    0);

                transform.localPosition = _originalPosition + shakeAmount;
                yield return null;
            }

            transform.localPosition = _originalPosition;
            onShakeEnd.Invoke();
        }

        /// <summary>
        /// Starts a new shake with a specified duration and vector intensity.
        /// </summary>
        /// <param name="duration">Duration of the shake.</param>
        /// <param name="intensity">Intensity of the shake on both axes.</param>
        public void Shake(float duration, Vector2 intensity)
        { 
            StopAllCoroutines(); // Stop any ongoing shake before starting a new one
            StartCoroutine(ShakeCoroutine(duration, intensity));
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
