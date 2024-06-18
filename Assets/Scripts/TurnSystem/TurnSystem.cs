using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurnSystem
{
    [CreateAssetMenu(fileName = "New Turn Data", menuName = "TurnDate")]
    public class TurnData : ScriptableObject
    {
        [Header("Data")]
        [SerializeField] private int currentTurn;
        [SerializeField] private int amountOfTurns;
        [SerializeField] private Vector3[] enemyPositions;

        public bool IsPlayerTurn { get; set; }
        public int CurrentTurn { get { return currentTurn; } set { currentTurn = value; } }
        public int AmountOfTurns { get { return amountOfTurns; } private set { amountOfTurns = value; } }
        public Vector3[] EnemyPositions { get { return enemyPositions; } set { enemyPositions = value; } }
    }
}