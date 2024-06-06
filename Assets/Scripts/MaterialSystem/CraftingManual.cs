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
        [Tooltip("Event to trigger when craft button is clicked")]
        [SerializeField] private UnityEvent onCraftButtonClicked;
        
        [Tooltip("Reference to the material system")]
        [SerializeField] private MaterialSystem materialSystem;
        
        
        [Tooltip("Button to confirm the crafting choice")]
        [SerializeField] private Button craftButton;
        
        private Recipe _selectedRecipe;
        
        private void Awake()
        {
            // Set up the craft button listener
            craftButton.onClick.AddListener(OnCraftButtonClicked);
        }
        
        /// <summary>
        /// Displays the selected recipe in the crafting manual
        /// </summary>
        /// <param name="recipe">The Recipe that was selected</param>
        public void DisplayRecipe(Recipe recipe)
        {
            _selectedRecipe = recipe;
            UpdateCraftButton();
        }
        
        private void UpdateCraftButton()
        {
            // Enable or disable the craft button based on the availability of materials
            craftButton.interactable = materialSystem.HasRequiredMaterials(_selectedRecipe);
        }
        
        private void OnCraftButtonClicked()
        {
            if (_selectedRecipe != null && materialSystem.HasRequiredMaterials(_selectedRecipe))
            {
                onCraftButtonClicked.Invoke();
            }
        }
    }

}