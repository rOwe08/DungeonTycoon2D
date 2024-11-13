using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenNewLevelButton : MonoBehaviour
{
    public Player player;

    public GameObject CellGate;
    public GameObject UnlockButton;
    public GameObject PriceText;

    public int Price;

    private void Start()
    {
        PriceText.GetComponent<TextMeshPro>().text = Price.ToString();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            if (player.Coins >= Price)
            {
                CellGate.GetComponent<Animator>().SetBool("IsOpening", true);

                player.OnCoinsChange(-Price);

                Destroy(UnlockButton);
                Destroy(gameObject);
            }
        }
    }
}
