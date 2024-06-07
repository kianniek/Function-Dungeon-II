using Towers;
using UnityEngine;

public class TowerBehaviorController : MonoBehaviour
{
    [SerializeField] private TowerVariables towerVariables;
    [SerializeField] private Transform rangeTransform;

    private int _range;

    private void Start()
    {
        _range = towerVariables.Range;
    }

    private void OnDrawGizmos()
    {
        // Draws range for debug purposes
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(rangeTransform.position, _range / 2);
    }
}
