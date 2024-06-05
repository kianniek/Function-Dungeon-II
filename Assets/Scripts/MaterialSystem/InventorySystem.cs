using Events.GameEvents.Typed;

namespace MaterialSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "NewInventorySystem", menuName = "Inventory/InventorySystem")]
    public class InventorySystem : ScriptableObject
    {
        [SerializeField] private List<Material> materials = new List<Material>();
        [SerializeField] private List<Ore> ores = new List<Ore>();
        
        [SerializeField] private ItemGameEvent onOreCollected;
        [SerializeField] private ItemGameEvent onOreUsed;
        
        [SerializeField] private ItemGameEvent onMaterialCollected;
        [SerializeField] private ItemGameEvent onMaterialUsed;
        
        public List<Material> Materials => materials;
        public List<Ore> Ores => ores;
        
        /// <summary>
        /// Adds a new material to the inventory
        /// </summary>
        /// <param name="material">Material Object that needs to be added to the Inventory</param>
        public void AddNewMaterial(Material material)
        {
            if (!materials.Contains(material))
            {
                materials.Add(material);
            }
        }
        
        /// <summary>
        /// Adds a new ore to the inventory
        /// </summary>
        /// <param name="ore">Ore Object that needs to be added to the Inventory</param>
        public void AddNewOre(Ore ore)
        {
            if (!ores.Contains(ore))
            {
                ores.Add(ore);
            }
        }
        
        /// <summary>
        /// Sets the material amount of an existing material
        /// </summary>
        /// <param name="material">Existing material that was collected</param>
        /// <param name="amount">Amount of a material that has been collected</param>
        public void CollectMaterial(Material material, int amount)
        {
            if (!materials.Contains(material))
            {
                AddNewMaterial(material);
            }
            
            material.Collect(amount);
            onMaterialCollected.Invoke(material);
        }
        
        /// <summary>
        /// Removes the amount of material from the inventory
        /// </summary>
        /// <param name="material">Material to be consumed</param>
        /// <param name="amount">Amount of the material to be subtracted</param>
        public void UseMaterial(Material material, int amount)
        {
            if (!materials.Contains(material)) return;
            material.Use(amount);
            onMaterialUsed.Invoke(material);
        }
        
        /// <summary>
        /// Collects the ore and adds its amount to the inventory
        /// </summary>
        /// <param name="ore">The type ore that is collected</param>
        /// <param name="amount">The amount of the ore that is collected</param>
        public void CollectOre(Ore ore, int amount)
        {
            if (!ores.Contains(ore))
            {
                AddNewOre(ore);
            }
            
            ore.Collect(amount);
            onOreCollected.Invoke(ore);
        }
        
        /// <summary>
        /// Removes the amount of ore from the inventory
        /// </summary>
        /// <param name="material">Ore to be consumed</param>
        /// <param name="amount">Amount of the ore to be subtracted</param>
        public void UseOre(Ore ore, int amount)
        {
            if (!ores.Contains(ore)) return;
            ore.Use(amount);
            onOreUsed.Invoke(ore);
        }
    }
}