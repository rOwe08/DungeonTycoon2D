using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPlaceScript : MonoBehaviour
{
    public Monster MonsterAssigned;
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

    private void HandleNPCAssignment(NPC npc)
    {
        if (npc is Monster monster)
        {
            MonsterAssigned = monster;
        }
    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            Monster monsterAssigned = MonsterAssigned ?? new Monster { Name = "rowe" };
            UIManager.Instance.OpenMonsterPlaceWindow(monsterAssigned);

            //CameraManager.Instance.MoveCameraToPoint(CameraPoint.transform);
        }
    }
}
