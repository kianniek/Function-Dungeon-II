using System.Collections;
using Events.GameEvents;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Robbe
{
    public class ShootArm : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private GameObject lineSystem;
        [SerializeField] private float projectileSpeed = 5f;

        [SerializeField] private Vector2GameEvent onHandShot;
        [SerializeField] private Vector2GameEvent onHitpointHit;
        [SerializeField] private GameEvent onHitpointMiss;

        private float _aValue;
        private float _bValue;
        private float _xValue = 20;
        private Vector2 _shootPoint;
        private Vector2 _targetPosition;
        private Transform _projecileTransform;

        void Start()
        {
            onHitpointHit.AddListener(BulletHitShot);
            onHitpointMiss.AddListener(BulletMissShot);
            _projecileTransform = Instantiate(projectilePrefab).transform;
        }

        /// <summary>
        /// Gets executed when bullet hits weakpoint (pre-determined by using discriminant)
        /// Moves bullet towards weakpoint
        /// </summary>
        /// <param name="weakpointPosition">Position of the weakpoint</param>
        private void BulletHitShot(Vector2 weakpointPosition)
        {
            _targetPosition = weakpointPosition;
            StartCoroutine(MoveTowardsTarget());

            lineSystem.transform.position = _targetPosition;
            _projecileTransform.gameObject.SetActive(false);
            _shootPoint = _targetPosition;
        }

        /// <summary>
        /// Gets executed when bullet misses weakpoint (pre-determined by using discriminant)
        /// Moves bullet towards position, uses y=ax+b to determine position
        /// </summary>
        private void BulletMissShot()
        {
            _targetPosition = new Vector2(_xValue, _xValue * _aValue + _bValue);
            StartCoroutine(MoveTowardsTarget());
        }

        /// <summary>
        /// Moves projectile towards target
        /// </summary>
        /// <returns></returns>
        private IEnumerator MoveTowardsTarget()
        {
            _projecileTransform.position = _shootPoint;

            var direction = _targetPosition - _shootPoint;

            //make the projectile look at the target
            _projecileTransform.right = direction;

            //while the projectile is not at the target keep moving
            while (Vector2.Distance(_projecileTransform.position, _targetPosition) > 0.1f)
            {
                _projecileTransform.position = Vector2.MoveTowards(_projecileTransform.position, _targetPosition, projectileSpeed * Time.deltaTime);
                yield return null;
            }
        }

        public void Shoot()
        {
            _projecileTransform.gameObject.SetActive(true);
            onHandShot.Invoke(new Vector2(_aValue, _bValue));
        }

        public void GetAValue(float aValue)
        {
            _aValue = aValue;
        }

        public void GetBValue(float bValue)
        {
            _projecileTransform.position = new Vector2(_projecileTransform.position.x, _bValue);
            _bValue = bValue;
        }
    }
}
