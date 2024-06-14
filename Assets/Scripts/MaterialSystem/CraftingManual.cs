using TMPro;
using UI;
using UnityEngine.Events;

namespace MaterialSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    
    /// <summary>
    /// Crafting manual class that handles the crafting manual UI
    /// </summary>
    public class CraftingManual : MonoBehaviour
    { 
        
        [Tooltip("Reference to the material system")]
        [SerializeField] private InventorySystem inventorySystem;
        
        
        [Tooltip("Button to confirm the crafting choice")]
        [SerializeField] private Button craftButton;
        
        /// <summary>
        /// Update the craft button based on the availability of materials for the selected recipe
        /// </summary>
        /// <param name="recipe">The Recipe that was selected</param>
        public void UpdateCraftButton(Recipe recipe)
        {
            // Enable or disable the craft button based on the availability of materials
            craftButton.interactable = inventorySystem.HasRequiredMaterials(recipe);
        }
    }

}