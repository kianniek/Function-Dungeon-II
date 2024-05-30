using System;
using UnityEngine;

namespace Delay
{
    /// <summary>
    /// Contains a MonoBehaviour, whether to set it active or not and a delay in seconds.
    /// </summary>
    [Serializable]
    public class MonoBehaviourDelayContainer
    {
        [SerializeField] private MonoBehaviour script;
        [SerializeField] private bool setActiveTo;
        [SerializeField] private float delayInSeconds;
        
        /// <summary>
        /// The MonoBehaviour to be enabled or disabled.
        /// </summary>
        public MonoBehaviour Script => script;
        
        /// <summary>
        /// Whether the MonoBehaviour is set active or not.
        /// </summary>
        public bool SetActiveTo => setActiveTo;
        
        /// <summary>
        /// The delay in seconds before the MonoBehaviour is enabled or disabled.
        /// </summary>
        public float DelayInSeconds => delayInSeconds;
    }
}