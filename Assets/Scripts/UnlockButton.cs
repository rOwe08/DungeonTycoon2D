using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButton : MonoBehaviour
{
    public GameObject ButtonToReveal;

    private void OnEnable()
    {
        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
    }

    public void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            ButtonToReveal.SetActive(!ButtonToReveal.activeSelf);
        }
    }
}
