using UnityEngine;

namespace MaterialSystem
{
    /// <summary>
    /// Abstract class that holds the base properties of an item.
    /// </summary>
    public abstract class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private Sprite icon;
        [SerializeField] private int amountCollected;
        [SerializeField] private int amountUsed;
        /// <summary>
        /// Name of the item
        /// </summary>
        public string ItemName => itemName;
        /// <summary>
        /// Icon of the item
        /// </summary>
        public Sprite Icon => icon;
        /// <summary>
        /// Amount of the item that has been collected
        /// </summary>
        public int AmountCollected => amountCollected;
        /// <summary>
        /// Amount of the item that has been used
        /// </summary>
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