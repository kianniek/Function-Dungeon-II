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
    private Vector2 _answer;

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
        var a = linearEquationTextModifier.AVariable;

        //Set x coordinate as range from tower and set y coordinate as range * a (ax). since B is not used
        _answer = new Vector2(shootingTowerVariables.Range/a, shootingTowerVariables.Range * a);

        Debug.Log(_answer.x + " " + _answer.y);

        bulletCoordinatesSet.Invoke(_answer);
    }
}
