using UnityEngine;
using UnityEngine.Events;

namespace Tabel
{
    public class OutputController : MonoBehaviour
    {
        [SerializeField] private UnityEvent onAwake;
        [SerializeField] private UnityEvent onStart;

        private void Awake()
        {
            onAwake.Invoke();
        }

        private void Start()
        {
            onStart.Invoke();
        }
    }
}