using Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using WorldGrid;

namespace Towers
{
    public class SuitablePlacementFinder : MonoBehaviour
    {
        [SerializeField] private Vector3Event onSuitablePlacement = new();
        
        private RaycastHit _onClickHit;

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.phase != InputActionPhase.Started)
                return;
            
            if (UnityEngine.Camera.main == null) 
                return;
            
            var ray = UnityEngine.Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            
            if (!Physics.Raycast(ray, out _onClickHit))
                return;
            
            if (!_onClickHit.collider.TryGetComponent<PlaceableTile>(out var tile))
                return;
            
            onSuitablePlacement.Invoke(tile.transform.position);
        }
        
        public void SubscribeToOnSuitablePlacement(UnityAction<Vector3> action)
        {
            onSuitablePlacement.AddListener(action);
        }
        
        public void UnsubscribeFromOnSuitablePlacement(UnityAction<Vector3> action)
        {
            onSuitablePlacement.RemoveListener(action);
        }
    }
}