using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Table
{
    [CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/BoolVariable")]
    public class BoolVariable : ScriptableObject
    {
        public bool Value { get; set; }
        
        private void Reset()
        {
            Value = false;
        }
        
        private void OnEnable()
        {
            Value = false;
        }
    }
}