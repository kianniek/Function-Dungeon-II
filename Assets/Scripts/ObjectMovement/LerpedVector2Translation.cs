using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ObjectMovement
{
    public class LerpedVector2Translation : MonoBehaviour
    {
        [SerializeField] private Vector3 moveDistance = new(10, 10);
        [SerializeField] private float movementTime = 1f;
        [SerializeField] private float movementSmoothing = 5f;
        [SerializeField] private bool baseOffStartPosition = true;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onMoveComplete = new();
        [SerializeField] private UnityEvent onMoveBackComplete = new();
        [SerializeField] private UnityEvent onMovingUp = new();
        [SerializeField] private UnityEvent onMovingDown = new();
        
        private Vector3 _startPosition;
        private Transform _selfTransform;
        private Vector3 _wantedPosition;
        
        private void Awake()
        {
            _selfTransform = transform;
            _startPosition = _selfTransform.localPosition;
            _wantedPosition = _startPosition;
        }
        
        public void MoveX()
        {
            StartCoroutine(MoveCoroutine(_startPosition + Vector3.right * moveDistance.x, onMoveComplete));
        }
        
        public void MoveY()
        {
            StartCoroutine(MoveCoroutine(_startPosition + Vector3.up * moveDistance.y, onMoveComplete));
        }
        
        public void MoveBackStartPosition()
        {
            StartCoroutine(MoveCoroutine(_startPosition, onMoveBackComplete));
        }
        
        /// <summary>
        /// Moves the object by the input value on the Y-axis
        /// </summary>
        /// <param name="input">Amount to deviate from the starting position</param>
        public void MoveYByInput(float input)
        {
            //Check if the input is positive or negative in reference to the previous input
            if(_wantedPosition.y < _startPosition.y + input)
                onMovingUp.Invoke();
            else
                onMovingDown.Invoke();
            
            //Set the wanted position to the new position
            _wantedPosition.y = input + _startPosition.y;
            StartCoroutine(LerpToWantedPosition());
        }
        
        public void MoveVector(Vector3 targetPosition)
        {
            StartCoroutine(MoveCoroutine(targetPosition, onMoveComplete));
        }
        
        private IEnumerator MoveCoroutine(Vector3 targetPosition, UnityEvent onCompleteEvent)
        {
            var elapsedTime = 0f;
            var initialPosition = _selfTransform.localPosition;
            
            while (elapsedTime < movementTime)
            {
                _selfTransform.localPosition =
                    Vector3.Lerp(initialPosition, targetPosition, elapsedTime / movementTime);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            _selfTransform.localPosition = targetPosition;

            onCompleteEvent?.Invoke();
        }
        
        private IEnumerator LerpToWantedPosition()
        {
            while (_selfTransform.localPosition != _wantedPosition)
            {
                _selfTransform.localPosition = Vector3.Lerp(_selfTransform.localPosition, _wantedPosition,
                    movementSmoothing * Time.deltaTime);
                yield return null;
            }
            
            _selfTransform.localPosition = _wantedPosition;
        }
    }
}