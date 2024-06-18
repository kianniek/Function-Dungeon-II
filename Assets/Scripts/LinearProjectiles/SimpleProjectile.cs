using UnityEngine;

namespace LinearProjectiles
{
    public class SimpleProjectile : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Vector3 _endPosition;

        public void Shoot(Vector3 endPosition)
        {
            _endPosition = endPosition;
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _endPosition,
                speed * Time.fixedDeltaTime
            );

            if (transform.position == _endPosition)
            {
                gameObject.SetActive(false);
            }
        }
    }
}