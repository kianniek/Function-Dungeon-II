using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Events.GameEvents;
using Events.GameEvents.Typed;
using UnityEngine;

namespace Kaijus
{
    public class KaijuHealth : MonoBehaviour
    {
        [SerializeField] private List<GameObject> weakpoints = new();

        [Header("Events")]
        [SerializeField] private Vector2GameEvent onHitpointHit;
        [SerializeField] private GameEvent onHitpointMiss;
        [SerializeField] private GameEvent onKajiuMiss;
        [SerializeField] private GameEvent onKaijuDie;

        private int _health;
        private int _missCount;

        private void Awake()
        {
            onHitpointHit.AddListener(SubtractHealth);
            onHitpointMiss.AddListener(CountCollisions);
        }

        private void OnDisable()
        {
            onHitpointHit.RemoveListener(SubtractHealth);
            onHitpointMiss.RemoveListener(CountCollisions);
        }

        /// <summary>
        /// Set kaiju health based on amount of hitpoints
        /// </summary>
        private void Start()
        {
            foreach (var weakpoint in weakpoints)
            {
                _health++;
            }
        }

        /// <summary>
        /// When hitpoint gets hit substract health
        /// </summary>
        private void SubtractHealth()
        {
            _health--;
            if(_missCount == 0)
            {
                _missCount = -1;
            }
            else
            {
                _missCount = 0;
            }
            DieCheck();
        }

        /// <summary>
        /// When health is zero invoke kaiju death event and destroy kaiju
        /// </summary>
        private void DieCheck()
        {
            if (_health == 0)
            {
                onKaijuDie.Invoke();
                Debug.Log("Dead");                
                Destroy(gameObject);
            }
        }

        private void CountCollisions()
        {
            _missCount++;

            if(_missCount == _health)
            {
                onKajiuMiss.Invoke();
                _missCount = 0;
            }
        }
    }
}
