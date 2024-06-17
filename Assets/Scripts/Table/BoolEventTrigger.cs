using UnityEngine;
using UnityEngine.Events;
using Events.GameEvents.Typed;

namespace Table
{
    public class BoolEventTrigger : MonoBehaviour
    {
        [Header("Bool Variable")] [SerializeField]
        private BoolVariable boolVariable;
        
        [Header("Bool Game Event")] [SerializeField]
        private BoolGameEvent boolGameEvent;
        
        [Header("Events")] [SerializeField] private UnityEvent onTrue;
        [SerializeField] private UnityEvent onFalse;
        
        private void OnEnable()
        {
            boolGameEvent.AddListener(OnEventTriggered);
        }
        
        private void OnDisable()
        {
            boolGameEvent.RemoveListener(OnEventTriggered);
        }
        
        public void OnEventTriggered(bool value)
        {
            boolVariable.Value = value;
        }
        
        public void CheckValueAndInvokeEvent()
        {
            if (boolVariable.Value)
            {
                onTrue.Invoke();
            }
            else
            {
                onFalse.Invoke();
            }
        }
    }
}