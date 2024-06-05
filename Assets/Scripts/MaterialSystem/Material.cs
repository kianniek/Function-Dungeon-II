namespace MaterialSystem
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "NewMaterial", menuName = "Crafting/Material")]
    public class Material : Item
    {
        [SerializeField] private string materialName;
        public string MaterialName => materialName;
        
    }

}