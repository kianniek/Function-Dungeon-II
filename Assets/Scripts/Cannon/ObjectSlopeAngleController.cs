using Events;
using UnityEngine;

namespace Cannon
{
    public class ObjectSlopeAngleController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject barrelRotationPivot;
        
        [Header("Settings")]
        [SerializeField] private float startSlope;
        
        [Header("Events")]
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
        
        // Rotate the barrel based on the value of slope
        private void Rotate(float slope)
        {
            var newAngle = GetAngle(slope);
            
            barrelRotationPivot.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            
            onAngleChange.Invoke(newAngle);
        }
        
        private void OnValidate()
        {
            if (barrelRotationPivot)
                Rotate(startSlope);
        }
        
        // Calculate the angle in degrees
        private static float GetAngle(float y)
        {
            return Mathf.Atan2(y, 1) * Mathf.Rad2Deg;
        }

    }
}