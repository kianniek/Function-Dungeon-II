using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutputController : MonoBehaviour
{

    [SerializeField] private UnityEvent onAwake;
    [SerializeField] private UnityEvent onStart;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        onAwake.Invoke();
    }
    // Start is called before the first frame update
    void Start()
    {
        onStart.Invoke();
    }
}
