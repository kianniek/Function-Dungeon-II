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
        [SerializeField] private Transform enemyPlace;

        public UnityEvent OnPlayerTurn = new UnityEvent();
        public UnityEvent OnEnemyTurn = new UnityEvent();
        public FloatEvent OnNewTurn = new FloatEvent();
        public UnityEvent OnMaxTurnsReached = new UnityEvent();

        private void Start()
        {
            ResetTurn();
        }

        /// <summary>
        /// Ends the current turn and switches to the opposite turn
        /// </summary>
        public void StartNewTurn()
        {
            turnData.IsPlayerTurn = !turnData.IsPlayerTurn;

            if (turnData.IsPlayerTurn)
            {
                OnPlayerTurn.Invoke();
                OnNewTurn.Invoke(GetCurrentTurn());
            }
            else
            {
                turnData.CurrentTurn++;
                OnEnemyTurn.Invoke();


                if (turnData.AmountOfTurns == turnData.CurrentTurn)
                {
                    OnMaxTurnsReached.Invoke();
                }
                else
                {
                    var enemyposition = turnData.EnemyPositions[turnData.CurrentTurn];
                    if (enemyposition != null)
                        enemyPlace.position = enemyposition;
                }
            }
        }

        /// <summary>
        /// Gets the current turn
        /// </summary>
        /// <returns>The current turn</returns>
        public int GetCurrentTurn()
        {
            return turnData.CurrentTurn + 1;
        }

        /// <summary>
        /// Resets the counter and starts a new one
        /// </summary>
        public void ResetTurn()
        {
            turnData.CurrentTurn = 0;
            turnData.IsPlayerTurn = false;
            enemyPlace.position = turnData.EnemyPositions[turnData.CurrentTurn];
            StartNewTurn();
        }
    }
}