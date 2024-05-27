using UnityEngine;

namespace LinearFunction
{
    [ExecuteInEditMode]
    public class AxisRotator : MonoBehaviour
    {
        private float _slopeValue;

        public float SlopeValue
        {
            get => _slopeValue;
            set
            {
                _slopeValue = value;
                Rotate();
            }
        }
        // Enum for the XYZ axes
        public enum Axis
        {
            X,
            Y,
            Z
        }

        // Public fields to be set in the Inspector
        [SerializeField] private Axis rotationAxis;
        [SerializeField] private Transform targetTransform;
        private float _rotationAngle;

        // Rotate the target Transform based on the selected axis and angle
        public void Rotate()
        {
            if (targetTransform == null)
            {
                Debug.LogWarning("Target Transform is not set.");
                return;
            }

            var rotationVector = Vector3.zero;

            _rotationAngle = FunctionToAngle.GetAngleFromSlope(_slopeValue);

            switch (rotationAxis)
            {
                case Axis.X:
                    rotationVector = new Vector3(_rotationAngle, 0, 0);
                    break;
                case Axis.Y:
                    rotationVector = new Vector3(0, _rotationAngle, 0);
                    break;
                case Axis.Z:
                    rotationVector = new Vector3(0, 0, _rotationAngle);
                    break;
            }

            targetTransform.Rotate(rotationVector);
        }

        // Example usage in the Start method
        private void Start()
        {
            Rotate();
        }
    }
}