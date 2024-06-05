using UnityEngine;

namespace MaterialSystem
{
    [System.Serializable]
    public class MaterialRequirement
    {
        [SerializeField] private Material material;
        [SerializeField] private int amount;
        
        public Material Material => material;
        public int Amount => amount;
    }
}