using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "TowerVariables", menuName = "Towers/TowerVariables", order = 0)]
    public class TowerVariables : ScriptableObject
    {
        [Tooltip("The range that a tower can shoot its projectiles")]
        [SerializeField] private int range;
        [Tooltip("The shooting speed of a tower")]
        [SerializeField] private float shootingSpeed;
        [Tooltip("The amount of flowers the tower costs to place")]
        [SerializeField] private int flowerCost;

        public int Range => range;

        public float ShootingSpeed => shootingSpeed;

        public int FlowerCost => flowerCost;
    }
}
