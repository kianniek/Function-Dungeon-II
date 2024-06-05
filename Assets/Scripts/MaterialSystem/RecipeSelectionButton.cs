using UI;
using UnityEngine;
using UnityEngine.UI;

namespace MaterialSystem
{
    /// <summary>
    /// Button to select a recipe in the crafting manual
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class RecipeSelectionButton : MonoBehaviour
    {
        
        [SerializeField] private Recipe recipe; // The recipe this button represents
        [SerializeField] private CraftingManual craftingManual; // Reference to the crafting manual
        
        [Header("Text Components")]
        [SerializeField] private ResourceText recipeNameText; // Text component to display the recipe name
        [SerializeField] private ResourceText recipeMaterialsText; // Text component to display the recipe materials
        
        private void Awake()
        {
            // Set up the button click listener
            GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        }
        
        private void Start()
        {
            // Set the text components
            recipeNameText.SetValue(recipe.RecipeName);
            
            var recipeDetails = recipe.GetRecipeDetails();
            
            foreach (var detail in recipeDetails)
            {
                recipeMaterialsText.SetValue(detail.Key, detail.Value.ToString(), true);
            }
        }
        
        private void OnButtonClicked()
        {
            // Display the selected recipe in the crafting manual
            craftingManual.DisplayRecipe(recipe);
        }
    }
}