using UnityEngine;

namespace Minecart
{
    public class RoundPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 offset; // The offset that will be added after rounding

        [SerializeField] private Transform transformToRound; // The transform that need to be rounded

        private void FixedUpdate()
        {
            var position = transform.position;

            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            position.z = Mathf.Round(position.z);

            transformToRound.position = position + offset;
        }
    }
}