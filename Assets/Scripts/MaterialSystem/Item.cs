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
        
        /// <summary>
        /// Keeps track of the amount of an item that has been collected.
        /// </summary>
        /// <param name="amount"></param>
        public void Collect(int amount)
        {
            amountCollected += amount;
        }
        
        /// <summary>
        /// Keeps track of the amount of an item that has been used. Does not remove the item from the inventory.
        /// </summary>
        /// <param name="amount"></param>
        public void Use(int amount)
        {
            amountUsed += amount;
        }
        
        /// <summary>
        /// Removes the item from the inventory.
        /// </summary>
        /// <param name="amount"></param>
        public void Remove(int amount)
        {
            amountCollected -= amount;
        }
    }
}