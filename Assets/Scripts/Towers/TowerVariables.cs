using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "TowerVariables", menuName = "Towers/TowerVariables", order = 0)]
    public class TowerVariables : ScriptableObject
    {
        [Tooltip("The amount of flowers the tower costs to place")]
        [SerializeField, Min(1)] private int flowerCost;
        
        [Header("Attack Settings")]
        [Tooltip("The shooting speed of a tower")]
        [SerializeField, Min(0.1f)] private float fireRate;
        [Tooltip("The fireRange that a tower can shoot its projectiles")]
        [SerializeField, Min(0.1f)] private int fireRange;

        public int FireRange => fireRange;

        public float FireRate => fireRate;

        public int FlowerCost => flowerCost;
    }
}
