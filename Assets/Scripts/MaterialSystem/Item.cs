using UnityEngine;

namespace MaterialSystem
{
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int amountCollected;
        [SerializeField] private int amountUsed;
        
        public string ItemName => itemName;
        public Sprite Icon => icon;
        public int AmountCollected => amountCollected;
        public int AmountUsed => amountUsed;
        
        public void Collect(int amount)
        {
            amountCollected += amount;
        }
        
        public void Use(int amount)
        {
            amountUsed += amount;
        }
    }
}