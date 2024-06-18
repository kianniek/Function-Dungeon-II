using System.Collections.Generic;
using Events.GameEvents;
using UnityEngine;

namespace Kaijus
{
    public class KaijuHealth : MonoBehaviour
    {
        [SerializeField] private List<GameObject> weakpoints = new();

        [Header("Events")]
        [SerializeField] private GameEvent onHitpointHit;
        [SerializeField] private GameEvent onKaijuDie;

        private int _health;

        private void Awake()
        {
            onHitpointHit.AddListener(SubtractHealth);
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
    }
}
