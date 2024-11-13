using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceScript : MonoBehaviour
{
    public Hero HeroAssigned;

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
            Hero heroToAssign = HeroAssigned ?? new Hero { Name = string.Empty };
            UIManager.Instance.OpenHeroPlaceWindow(heroToAssign);
        }
    }
}
