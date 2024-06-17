using System.Collections.Generic;
using MaterialSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.InventoryDisplay
{
    public class InventoryDisplay : MonoBehaviour
    {
        private const string ItemEntryName = "ItemEntry";
        
        [SerializeField] private InventorySystem inventorySystem;
        
        [Header("Template")]
        [SerializeField] private GameObject ItemEntryTemplate;
        [SerializeField] private Image itemIconField;
        [SerializeField] private TMP_Text itemNameField;
        [SerializeField] private TMP_Text itemAmountField;
        
        [SerializeField] private Transform contentPanel;
        
        private void Start()
        {
            DisplayInventory();
        }
        
        private void DisplayInventory()
        {
            //set all children to inactive
            foreach (Transform child in contentPanel.transform)
            {
                child.gameObject.SetActive(false);
            }
            
            var materials = inventorySystem.Materials;
            var ores = inventorySystem.Ores;
            
            foreach (var material in materials)
            {
                CreateItemEntry(material.ItemName, material.Icon, material.AmountCollected - material.AmountUsed);
            }
            
            foreach (var ore in ores)
            {
                CreateItemEntry(ore.ItemName, ore.Icon, ore.AmountCollected - ore.AmountUsed);
            }
        }
        
        private void CreateItemEntry(string itemName, Sprite icon, int amount)
        {
            var itemEntryInstance = Instantiate(ItemEntryTemplate, contentPanel.transform);
            itemEntryInstance.name = ItemEntryName;
            
            //remove all children from the item entry template
            foreach (Transform child in itemEntryInstance.transform)
            {
                Destroy(child.gameObject);
            }
            
            var itemIconFieldInstance = Instantiate(itemIconField, itemEntryInstance.transform);
            var itemNameFieldInstance = Instantiate(itemNameField, itemEntryInstance.transform);
            var itemAmountFieldInstance = Instantiate(itemAmountField, itemEntryInstance.transform);
            
            itemIconFieldInstance.sprite = icon;
            itemNameFieldInstance.text = itemName;
            itemAmountFieldInstance.text = amount.ToString();
            
            itemEntryInstance.SetActive(true); // Ensure the new item is visible
        }
    }
}