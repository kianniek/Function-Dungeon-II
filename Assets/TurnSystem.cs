using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace TurnSystem
{
    public class TurnSystem : MonoBehaviour
    {
        [SerializeField] private TurnData turnData;

        public BoolEvent OnPlayerTurn = new BoolEvent();
        public Vector3Event OnEnemyTurn = new Vector3Event();
        public UnityEvent OnMaxTurnsReached = new UnityEvent();

        private void Start()
        {
            StartNewTurn();
        }

        /// <summary>
        /// Ends the current turn and switches to the opposite turn
        /// </summary>
        public void StartNewTurn()
        {
            OnMaxTurnsReached.Invoke();
            turnData.IsPlayerTurn = !turnData.IsPlayerTurn;

            if (turnData.IsPlayerTurn)
            {
                turnData.CurrentTurn++;

                if (turnData.AmountOfTurns < turnData.CurrentTurn)
                {
                    OnMaxTurnsReached.Invoke();
                }
                else
                {
                    OnPlayerTurn.Invoke(turnData.IsPlayerTurn);
                }
            }
            else
            {
                var enemyposition = turnData.EnemyPositions[turnData.CurrentTurn - 1];
                OnEnemyTurn.Invoke(enemyposition);
            }
        }

        /// <summary>
        /// Gets the current turn
        /// </summary>
        /// <returns>The current turn</returns>
        public int GetCurrentTurn()
        {
            return turnData.CurrentTurn;
        }
    }
}