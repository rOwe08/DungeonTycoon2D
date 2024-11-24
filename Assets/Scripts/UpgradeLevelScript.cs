using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeLevelScript : MonoBehaviour
{
    public LevelManager levelManager;
    private Player player; 
    public TextMeshPro levelText;

    void Start()
    {
        // Подписываемся на событие клика
        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
        player = levelManager.player;
        ChangeLevelText();
    }

    void HandleTouchOrClick(Vector3 worldPosition)
    {
        // Проверяем, попадает ли клик в коллайдер объекта
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            // Проверка, что у игрока достаточно монет для апгрейда
            if (player.GetResource(ResourceType.Coins).Amount >= levelManager.costForNextLevel)
            {
                player.OnResourceChange(ResourceType.Coins, -levelManager.costForNextLevel);
                levelManager.UpLevel();
                ChangeLevelText();

                Debug.Log("Level upgraded!");
            }
            else
            {
                Debug.Log("Not enough coins for upgrade!");
            }
        }
    }

    void ChangeLevelText()
    {
        levelText.text = "Level " + levelManager.level.ToString();
    }
}
