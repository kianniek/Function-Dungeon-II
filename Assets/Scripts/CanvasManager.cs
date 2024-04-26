using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField]
    private TextMeshProUGUI _enemyText;
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
        _enemyText.text = $"Enemies killed: {GameManager.instance._enemyKillCount} / {GameManager.instance._enemyCount}";
    }
}
