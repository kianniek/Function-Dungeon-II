using Events;
using UnityEngine;
using Utils;

namespace ObjectMovement
{
    [ExecuteInEditMode]
    public class ObjectSlopeAngleController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject rotationPivot;
        
        [Header("Settings")]
        [SerializeField] private float startSlope;
        
        [Header("Events")]
        [SerializeField] private FloatEvent onSlopeChange = new();
        [SerializeField] private FloatEvent onAngleChange = new();
        
        private void Start()
        {
            Rotate(startSlope);
        }
        
        private void OnValidate()
        {
            if (rotationPivot)
                Rotate(startSlope);
        }
        
        /// <summary>
        /// Rotates the object based on the coefficient of the x-term.
        /// </summary>
        /// <param name="slope"> The coefficient of the x-term. </param>
        public void Rotate(float slope)
        {
            onSlopeChange.Invoke(slope);
            
            var newAngle = MathExtensions.AToDegrees(slope);
            
            rotationPivot.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            
            onAngleChange.Invoke(newAngle);
        }
    }
}