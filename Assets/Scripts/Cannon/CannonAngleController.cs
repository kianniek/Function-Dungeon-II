using TypedUnityEvent;
using UnityEngine;

namespace Cannon
{
    public class CannonAngleController : MonoBehaviour
    {
        [SerializeField] private GameObject barrelRotationPivot;
        [SerializeField] private float a;
        [SerializeField] private FloatEvent OnAngleChange = new();

        public float A
        {
            get { return a; }
            set
            {
                a = value;
                Rotate();
            }
        }


        /// <summary>
        /// Rotate the barrel based on the value of _a
        /// </summary>
        private void Rotate()
        {
            var newAngle = GetAngle(a);
            barrelRotationPivot.transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
            OnAngleChange.Invoke(newAngle);
        }

        /// <summary>
        /// Calculate the angle in degrees
        /// </summary>
        /// <param name="a">The Y coordinate of linear function</param>
        /// <returns>The calculated angle in degrees</returns>
        private float GetAngle(float y)
        {
            float x = 1;
            return Mathf.Atan2(y * x, x) * Mathf.Rad2Deg;
        }

        private void OnValidate()
        {
            if (barrelRotationPivot != null)
                Rotate();
        }
    }
}