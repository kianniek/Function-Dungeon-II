using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MaterialSystem
{
    public class InventorySystemHandler : MonoBehaviour
    {
        [SerializeField] private InventorySystem inventorySystem;
        
        /// <summary>
        /// Adds a new material to the inventory
        /// </summary>
        /// <param name="material"></param>
        public void AddNewMaterial(Material material)
        {
            inventorySystem.AddNewMaterial(material);
        }
        
        /// <summary>
        /// Adds a new ore to the inventory
        /// </summary>
        /// <param name="ore"></param>
        public void AddNewOre(Ore ore)
        {
            inventorySystem.AddNewOre(ore);
        }
        
        /// <summary>
        /// Collects the material and adds it to the inventory
        /// </summary>
        /// <param name="material"></param>
        public void CollectMaterial(Material material)
        {
            inventorySystem.CollectMaterial(material);
        }
        
        /// <summary>
        /// Uses the material and removes it from the inventory
        /// </summary>
        /// <param name="material"></param>
        public void UseMaterial(Material material)
        {
            inventorySystem.UseMaterial(material);
        }
        
        /// <summary>
        ///  Collects the ore and adds it to the inventory
        /// </summary>
        /// <param name="ore"></param>
        public void CollectOre(Ore ore)
        {
            inventorySystem.CollectOre(ore);
        }
        
        /// <summary>
        /// Uses the ore and removes it from the inventory
        /// </summary>
        /// <param name="ore"></param>
        public void UseOre(Ore ore)
        {
            inventorySystem.UseOre(ore);
        }
        
        /// <summary>
        /// Checks if the inventory has the required materials for the recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        public bool HasRequiredMaterials(Recipe recipe)
        {
            return inventorySystem.HasRequiredMaterials(recipe);
        }
        
        /// <summary>
        /// Checks if the inventory has the required materials for the recipe
        /// </summary>
        /// <param name="material"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool HasAmountOfMaterial(Material material, int amount)
        {
            return inventorySystem.HasAmountOfMaterial(material, amount);
        }
    }

}