using Events.GameEvents.Typed;
using Extensions;
using TMPro;
using Towers;
using UI.LinearEquation;
using UnityEngine;

public class ShootingEquationSetter : MonoBehaviour
{
    [Header("Tower variables")]
    [SerializeField] private TowerVariables shootingTowerVariables;

    [Header("GUI References")]
    [SerializeField] private LinearEquationTextModifier linearEquationTextModifier;

    [Header("Events")]
    [SerializeField] Vector2GameEvent bulletCoordinatesSet;
    [SerializeField] private GameObjectGameEvent onShootingTowerPlaced;

    private GameObject _shootingTower;

    private void Awake()
    {
        onShootingTowerPlaced.AddListener(SetBombtower);
    }

    /// <summary>
    /// Retrieve shootingtower from event
    /// </summary>
    /// <param name="shootingTower">shootingtower retrieved from event</param>
    private void SetBombtower(GameObject shootingTower)
    {
        _shootingTower = shootingTower;
    }

    /// <summary>
    /// Check if typed coordinates are in range of tower and invoke the event to send coordinates back to tower
    /// </summary>
    public void OnConfirmButtonClicked()
    {
        var startPoint = new Vector2(_shootingTower.transform.position.x, _shootingTower.transform.position.y);
        var a = linearEquationTextModifier.AVariable;
        var direction = new Vector2(1, a);
        var normalizedDirection = direction.normalized;
        var scaledDirection = normalizedDirection * shootingTowerVariables.Range;
        var endpoint = startPoint + scaledDirection;

        Debug.Log(endpoint.x + " " + endpoint.y);

        bulletCoordinatesSet.Invoke(endpoint);
    }
}
