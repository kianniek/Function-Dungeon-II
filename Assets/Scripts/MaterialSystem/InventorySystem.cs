using Events.GameEvents.Typed;

namespace MaterialSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class InventorySystem : MonoBehaviour
    {
        [SerializeField] private List<Material> materials = new List<Material>();
        [SerializeField] private List<Ore> ores = new List<Ore>();
        
        [SerializeField] private ItemGameEvent onOreCollected;
        [SerializeField] private ItemGameEvent onOreUsed;
        
        
        [SerializeField] private ItemGameEvent onMaterialCollected;
        [SerializeField] private ItemGameEvent onMaterialUsed;
        
        // Method to add a new material type
        public void AddNewMaterial(Material material)
        {
            if (!materials.Contains(material))
            {
                materials.Add(material);
            }
        }
        
        // Method to add a new ore type
        public void AddNewOre(Ore ore)
        {
            if (!ores.Contains(ore))
            {
                ores.Add(ore);
            }
        }
        
        // Method to collect materials
        public void CollectMaterial(Material material, int amount)
        {
            if (materials.Contains(material))
            {
                material.Collect(amount);
                onMaterialCollected.Invoke(material);
            }
        }
        
        // Method to use materials
        public void UseMaterial(Material material, int amount)
        {
            if (materials.Contains(material))
            {
                material.Use(amount);
                onMaterialUsed.Invoke(material);
            }
        }
        
        // Method to collect ores
        public void CollectOre(Ore ore, int amount)
        {
            if (ores.Contains(ore))
            {
                ore.Collect(amount);
                onOreCollected.Invoke(ore);
            }
        }
        
        // Method to use ores
        public void UseOre(Ore ore, int amount)
        {
            if (ores.Contains(ore))
            {
                ore.Use(amount);
                onOreUsed.Invoke(ore);
            }
        }
    }
}