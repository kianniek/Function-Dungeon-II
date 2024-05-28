using System;
using UnityEngine;

namespace Delay
{
    [Serializable]
    public class MonoBehaviourDelayContainer
    {
        [SerializeField] private MonoBehaviour script;
        [SerializeField] private bool setActiveTo;
        [SerializeField] private float delayInSeconds;
        
        public MonoBehaviour Script => script;
        
        public bool SetActiveTo => setActiveTo;
        
        public float DelayInSeconds => delayInSeconds;
    }
}