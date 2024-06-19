using System;
using System.Collections;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Robbe
{
    public class ShootArm : MonoBehaviour
    {
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private float projectileSpeed = 5f;

        [SerializeField] private Vector2GameEvent onHandShot;
        [SerializeField] private Vector2GameEvent onHitpointHit;

        private bool _hitpointHit;
        private float _aValue;
        private float _bValue;
        private Vector2 _shootPoint;
        private Transform _projecileTransform;

        void Start()
        {
            onHitpointHit.AddListener(FreezeBulletPosition);
            _projecileTransform = Instantiate(projectilePrefab).transform;
        }

        private void FreezeBulletPosition(Vector2 position)
        {
            _hitpointHit = false; 
            _projecileTransform.position = position;
        }

        public void Shoot()
        {
            _hitpointHit = true;
            onHandShot.Invoke(new Vector2(_aValue, _bValue));
            StartCoroutine(ShootToTarget());
        }

        public void GetAValue(float aValue)
        {
            _aValue = aValue;
        }

        public void GetBValue(float bValue)
        {
            _bValue = bValue;
        }

        private IEnumerator ShootToTarget()
        {
            _hitpointHit = true;
            _projecileTransform.position = _shootPoint;
            Vector2 targetPosition = new Vector2(20, 20 * _aValue + _bValue);
            var direction = targetPosition - _shootPoint;

            //make the projectile look at the target
            _projecileTransform.right = direction;

            //while the projectile is not at the target keep moving
            while (_hitpointHit && Vector2.Distance(_projecileTransform.position, targetPosition) > 0.1f)
            {
                _projecileTransform.position = Vector2.MoveTowards(_projecileTransform.position, targetPosition, projectileSpeed * Time.deltaTime);
                yield return null;
            }
        }
    }
}
