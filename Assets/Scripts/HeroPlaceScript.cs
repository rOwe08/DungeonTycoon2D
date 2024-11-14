using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceScript : MonoBehaviour
{
    public Hero HeroAssigned;
    public GameObject CameraPoint;

    private void Start()
    {
        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
        GameManager.Instance.OnNPCAssigned.AddListener(HandleNPCAssignment);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
        GameManager.Instance.OnNPCAssigned.RemoveListener(HandleNPCAssignment);
    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            Hero heroToAssign = HeroAssigned ?? new Hero { Name = "rowe" };
            UIManager.Instance.OpenHeroPlaceWindow(heroToAssign);

            //CameraManager.Instance.MoveCameraToPoint(CameraPoint.transform);
        }
    }

    private void HandleNPCAssignment(NPC npc)
    {
        if (npc is Hero hero)
        {
            HeroAssigned = hero;
        }
    }
}
