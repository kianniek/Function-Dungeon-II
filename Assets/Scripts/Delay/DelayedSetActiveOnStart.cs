using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delay
{
    /// <summary>
    /// Activates or deactivates a script after a delay on start.
    /// </summary>
    public class DelayedSetActiveOnStart : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviourDelayContainer> scripts = new();
        
        private void Start()
        {
            foreach (var script in scripts)
            {
                StartCoroutine(DelayedEnable(script.Script, script.DelayInSeconds, script.SetActiveTo));
            }
        }
        
        private static IEnumerator DelayedEnable(MonoBehaviour script, float delay, bool setActive)
        {
            yield return new WaitForSeconds(delay);
            
            script.enabled = setActive;
        }
    }
}