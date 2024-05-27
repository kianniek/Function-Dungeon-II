using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Delay
{
    public class DelayedEnableOnStart : MonoBehaviour
    {
        [SerializeField] private List<MonoBehaviourDelayContainer> scripts = new();
        
        private void Start()
        {
            foreach (var script in scripts)
            {
                StartCoroutine(DelayedEnable(script.Script, script.DelayInSeconds));
            }
        }
        
        private static IEnumerator DelayedEnable(MonoBehaviour script, float delay)
        {
            yield return new WaitForSeconds(delay);
            
            script.enabled = true;
        }
    }
}