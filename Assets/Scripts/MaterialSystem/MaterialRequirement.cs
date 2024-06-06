using UnityEngine;

namespace MaterialSystem
{
    /// <summary>
    /// Material requirement class that holds the required materials for crafting an item
    /// </summary>
    [System.Serializable]
    public class MaterialRequirement
    {
        [SerializeField] private Material material;
        [SerializeField] private int amount;
        
        /// <summary>
        /// Material object that is required
        /// </summary>
        public Material Material => material;
        
        /// <summary>
        /// Amount of material that is required
        /// </summary>
        public int Amount => amount;
    }
}