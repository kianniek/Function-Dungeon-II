namespace MaterialSystem
{
    using System.Collections.Generic;
    using UnityEngine;
    
    public class MaterialSystem : MonoBehaviour
    {
        [SerializeField] private List<MaterialEntry> playerMaterials;
        
        public bool HasRequiredMaterials(Recipe recipe)
        {
            foreach (var req in recipe.Materials)
            {
                var hasMaterial = false;
                foreach (var mat in playerMaterials)
                {
                    if (mat.material != req.material || mat.amount < req.amount) continue;
                    hasMaterial = true;
                    break;
                }
                if (!hasMaterial) return false;
            }
            return true;
        }
    }
    
    

}