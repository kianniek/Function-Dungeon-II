using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MaterialSystem
{
    [CreateAssetMenu(fileName = "NewRecipe", menuName = "Crafting/Recipe")]
    public class Recipe : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private List<MaterialRequirement> materials;
        
        public List<MaterialRequirement> Materials => materials;
        
        public string RecipeName => itemName;
    
        /// <summary>
        /// Returns the recipe details
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetRecipeDetails()
        {
            return materials.ToDictionary(material => material.Material.ItemName, material => material.Amount);
        }
    }
}