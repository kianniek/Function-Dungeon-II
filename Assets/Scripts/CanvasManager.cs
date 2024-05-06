using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class CanvasManager : MonoBehaviour
{
    public UnityEvent onAllEnemiesKilledEvent;
    public static CanvasManager instance;

    [SerializeField]
    private TextMeshProUGUI enemyText;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        enemyText.text = $"Enemies killed: {GameManager.instance._enemyKillCount} / {GameManager.instance._enemyCount}";
        if(GameManager.instance._enemyCount == GameManager.instance._enemyKillCount)
        {
            onAllEnemiesKilledEvent.Invoke();
    }
}
