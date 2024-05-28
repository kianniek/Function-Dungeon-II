using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectMover
{
    public class LerpedXMover : MonoBehaviour
    {
        [SerializeField] private float moveDistance = 10f;
        [SerializeField] private float movementTime = 1f;
        [SerializeField] private UnityEvent onMoveComplete;
        [SerializeField] private UnityEvent onMoveBackComplete;

        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.localPosition;
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
            var initialPosition = transform.localPosition;

            while (elapsedTime < movementTime)
            {
                transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / movementTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.localPosition = targetPosition;
            onCompleteEvent?.Invoke();
        }
    }
}