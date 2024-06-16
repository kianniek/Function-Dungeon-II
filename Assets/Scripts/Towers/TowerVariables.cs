using Attacks;
using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "TowerVariables", menuName = "Towers/TowerVariables", order = 0)]
    public class TowerVariables : ScriptableObject
    {
        [Tooltip("The amount of flowers the tower costs to place")]
        [SerializeField] private int flowerCost;
        
        [Header("Attack Settings")]
        [Tooltip("The shooting speed of a tower")]
        [SerializeField] private float fireRate;
        [Tooltip("The fireRange that a tower can shoot its projectiles")]
        [SerializeField] private int fireRange;

        public int FireRange => fireRange;

        public float FireRate => fireRate;

        public int FlowerCost => flowerCost;
    }
}
