using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectMover
{
    public class LerpedXMover : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 10f;
        [SerializeField] private float movementTime = 1f;
        [SerializeField] private UnityEvent onMoveComplete = new();
        [SerializeField] private UnityEvent onMoveBackComplete = new();

        private Vector3 _startPosition;
        private Transform _selfTransform;

        private void Awake()
        {
            _selfTransform = transform;
            _startPosition = _selfTransform.localPosition;
        }

        public void Move()
        {
            StartCoroutine(MoveCoroutine(_startPosition + Vector3.right * moveDistance, onMoveComplete));
        }

        public void MoveBack()
        {
            StartCoroutine(MoveCoroutine(_startPosition, onMoveBackComplete));
        }

        private IEnumerator MoveCoroutine(Vector3 targetPosition, UnityEvent onCompleteEvent)
        {
            var elapsedTime = 0f;
            var initialPosition = _selfTransform.localPosition;

            while (elapsedTime < movementTime)
            {
                _selfTransform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / movementTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _selfTransform.localPosition = targetPosition;
            onCompleteEvent?.Invoke();
        }
    }
}