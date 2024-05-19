using System.Collections.Generic;
using Events.GameEvents;
using Targets;
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
        
        private void Awake()
        {
            _enemyText = GetComponent<TMP_Text>();
        }
        
        private void Start()
        {
            UpdateEnemyLeftText();
        }
        
        private void OnValidate()
        {
            foreach (var script in targets)
            {
                if (!script) 
                    continue;
                
                script.OnDieEvent.RemoveListener(UpdateEnemyLeftText);
                script.OnDieEvent.AddListener(UpdateEnemyLeftText);
            }
        }
        
        private void UpdateEnemyLeftText()
        {
            _enemyText.text = string.Format(textFormat, targets.Count);
            
            if (targets.Count == 0)
            {
                onAllTargetsDestroyed?.Invoke();
            }
            else
            {
                onTargetDestroyed?.Invoke();
            }
        }
    }
}
