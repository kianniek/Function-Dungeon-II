using System;
using UnityEngine;

namespace Delay
{
    [Serializable]
    public class MonoBehaviourDelayContainer
    {
        [SerializeField] private MonoBehaviour script;
        [SerializeField] private float delayInSeconds;
        
        public MonoBehaviour Script => script;
        
        public float DelayInSeconds => delayInSeconds;
    }
}