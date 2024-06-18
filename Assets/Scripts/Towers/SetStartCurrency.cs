using FlowerSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Towers
{
    public class SetStartCurrency : MonoBehaviour
    {
        [Header("Currency")]
        [SerializeField] private FlowerCounter flowerCounter;
        [SerializeField] private int startCurrency = 100;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onCurrencySet = new();
        
        private void Start()
        {
            flowerCounter.CurrentFlowerCount = startCurrency;
        }
    }
}
