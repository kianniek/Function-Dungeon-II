using System.Collections.Generic;
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
            var details = new Dictionary<string, int>();
            
            foreach (var material in materials)
            {
                details.Add(material.material.ItemName , material.amount);
            }
            
            return details;
        }
    }
}