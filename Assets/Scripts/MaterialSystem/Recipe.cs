using System.Collections.Generic;
using UnityEngine;

namespace MaterialSystem
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private List<MaterialRequirement> materials;
        
        //private set for materials
        public List<MaterialRequirement> Materials
        {
            get;
            private set;
        }
        
        public string GetRecipeDetails()
        {
            var details = $"Recipe for {itemName}:\n";
            
            foreach (var material in materials)
            {
                details += $"{material.material.MaterialName}: {material.amount}\n";
            }
            
            return details;
        }
    }
}