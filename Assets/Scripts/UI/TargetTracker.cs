using System.Collections.Generic;
using System.Linq;
using Events.GameEvents;
using Health;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class TargetTracker : MonoBehaviour
    {
        [SerializeField] private List<Damageable> targets = new();
        
        [Header("Text Format")]
        [SerializeField] private string textFormat = "Enemies left: {0}";
        
        [Header("Events")]
        [SerializeField] private GameEvent onAllTargetsDestroyed; 
        [SerializeField] private GameEvent onTargetDestroyed;
        
        private TMP_Text _enemyText;
        private int _currentTargetCount;
        
        private void OnEnable()
        {
            foreach (var script in targets.Where(script => script))
            {
                script.SubscribeToDeathEvent(DestroyedTarget);
            }
        }
        
        private void OnDisable()
        {
            foreach (var script in targets.Where(script => script))
            {
                script.UnsubscribeFromDeathEvent(DestroyedTarget);
            }
        }
        
        public int CurrentTargetCount
        {
            get => _currentTargetCount;
            private set
            {
                if (value < 0)
                    return;
                
                _currentTargetCount = value;
                
                _enemyText.text = string.Format(textFormat, value);
            }
        }
        
        private void Awake()
        {
            _enemyText = GetComponent<TMP_Text>();
        }
        
        private void Start()
        {
            CurrentTargetCount = targets.Count;
        }
        
        private void DestroyedTarget()
        {
            CurrentTargetCount--;
            
            onTargetDestroyed?.Invoke();
            
            if (CurrentTargetCount == 0)
                onAllTargetsDestroyed?.Invoke();
        }
    }
}
