using Towers;
using UnityEngine;
using UnityEngine.InputSystem;

public class BombPositionSetter : MonoBehaviour
{
    [SerializeField] private TowerVariables bombTowerVariables;

    private Vector2 _mousePosition;

    [SerializeField] private GameObject bombTower;

    public InputAction MousePosition;

    private void FixedUpdate()
    {
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        _mousePosition = Mouse.current.position.ReadValue();

        if (_mousePosition.x < bombTower.transform.position.x - bombTowerVariables.Range || _mousePosition.x > bombTower.transform.position.x + bombTowerVariables.Range || _mousePosition.y < bombTower.transform.position.x - bombTowerVariables.Range || _mousePosition.y > bombTower.transform.position.x + bombTowerVariables.Range)
            return;

        Debug.Log(_mousePosition.x);
        Debug.Log(_mousePosition.y);
    }
}
