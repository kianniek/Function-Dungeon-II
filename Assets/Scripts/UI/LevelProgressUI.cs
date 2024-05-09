using System.Collections.Generic;
using Targets;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class LevelProgressUI : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAllEnemiesKilledEvent = new();

        [SerializeField] private TextMeshProUGUI enemyText;

        [SerializeField] private List<HitScript> enemies = new List<HitScript>();

        private int _enemyStartCount;
        private int _enemyKillCount = -1;

        private void Start()
        {
            _enemyStartCount = enemies.Count;
            UpdateEnemyLeftText();
        }
        private void OnValidate()
        {
            foreach (HitScript script in enemies)
            {
                script.OnDieEvent.RemoveListener(UpdateEnemyLeftText);
                script.OnDieEvent.AddListener(UpdateEnemyLeftText);
            }
        }

        private void UpdateEnemyLeftText()
        {
            _enemyKillCount++;
            enemyText.text = $"Enemies killed: {_enemyKillCount} / {_enemyStartCount}";
            if (_enemyStartCount == _enemyKillCount)
            {
                onAllEnemiesKilledEvent.Invoke();
            }
        }
    }
}
