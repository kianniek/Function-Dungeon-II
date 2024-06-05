using UnityEngine.Serialization;

namespace MaterialSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    
    //scriptable object
    [CreateAssetMenu(fileName = "MaterialEntry", menuName = "Material System/Material Entry", order = 0)]
    public class MaterialSystem : ScriptableObject
    {
        [SerializeField] private InventorySystem inventory;
        
        /// <summary>
        /// Adds a material to the player's inventory.
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public bool HasRequiredMaterials(Recipe recipe)
        {
            foreach (var req in recipe.Materials)
            {
                var hasMaterial = false;
                foreach (var mat in inventory.Materials)
                {
                    if (mat != req.Material || mat.AmountCollected < req.Amount) continue;
                    hasMaterial = true;
                    break;
                }
                if (!hasMaterial) return false;
            }
            return true;
        }
        
        /// <summary>
        /// Removes the required materials from the player's inventory.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool HasAmountOfMaterial(Material material, int amount)
        {
            foreach (var mat in inventory.Materials)
            {
                if (mat == material && mat.AmountCollected >= amount)
                {
                    return true;
                }
            }
            return false;
        }
    }
    
    

}