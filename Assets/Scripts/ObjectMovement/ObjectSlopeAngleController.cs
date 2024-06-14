using Events;
using UnityEngine;

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

        private float _slope;
        
        public float Slope
        {
            get => _slope;
            set
            {
                _slope = value;
                
                Rotate(value);
            }
        }
        
        private void Start()
        {
            Rotate(startSlope);
        }

        /// <summary>
        /// Sets the slope of the object
        /// </summary>
        /// <param name="input"></param>
        public void SetSlope(float input)
        {
            Slope = input;
        }
        
        // Rotate the barrel based on the value of slope
        private void Rotate(float slope)
        {
            onSlopeChange.Invoke(slope);
            
            var newAngle = GetAngle(slope);
            
            rotationPivot.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            
            onAngleChange.Invoke(newAngle);
        }
        
        private void OnValidate()
        {
            if (rotationPivot)
                Rotate(startSlope);
        }
        
        // Calculate the angle in degrees
        private static float GetAngle(float y)
        {
            return Mathf.Atan2(y, 1) * Mathf.Rad2Deg;
        }

    }
}