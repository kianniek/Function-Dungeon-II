using UnityEngine;

namespace Crafter
{
    public class InterceptionCalculator : MonoBehaviour
    {
        [SerializeField] private FunctionLineController line1;
        [SerializeField] private FunctionLineController line2;

        public Vector2 intersection;

        private float _a1;
        private float _b1;

        private float _a2;
        private float _b2;

        private void Start()
        {
            _a1 = line1.A;
            _b1 = line1.transform.position.y;

            _a2 = line2.A;
            _b2 = line2.transform.position.y;

            intersection = Vector2Extension.FindIntersection(_a1, _b1, _a2, _b2);
        }
    }
}
