using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlaceScript : MonoBehaviour
{
    public NPC AssignedNPC;
    public GameObject CameraPoint;
    private GameObject _spawnedNPC;

    public bool isHeroPlace;
    private Animator _npcAnimator;

    private void Start()
    {
        // Determine if this place is for a hero or a monster
        if (gameObject.name.ToLower().Contains("hero"))
        {
            isHeroPlace = true;
        }
        else if (gameObject.name.ToLower().Contains("monster"))
        {
            isHeroPlace = false;
        }

        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);

        if (_spawnedNPC != null)
        {
            _npcAnimator = _spawnedNPC.GetComponent<Animator>();  // Get Animator for the spawned NPC
        }
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
    }

    // Set NPC state (attacking or waiting)
    public void SetNPCState(bool isAttacking)
    {
        if (_npcAnimator != null)
        {
            _npcAnimator.SetBool("IsAttacking", isAttacking);  // Set attack animation flag
            _npcAnimator.SetBool("IsWaiting", !isAttacking);   // If not attacking, switch to waiting state
        }
    }

    // Set NPC hurting state
    public void SetHurtingState(bool isHurting)
    {
        if (_npcAnimator != null)
        {
            _npcAnimator.SetBool("IsHurting", isHurting);  // Set hurting animation flag
        }
    }

    public void HandleNPCAssignment(NPC npc)
    {
        AssignedNPC = npc;
        if (AssignedNPC != null)
        {
            SpawnNPC(AssignedNPC);
            this.transform.Find("MainImage").gameObject.SetActive(false);
            this.transform.Find("BoarderImage").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("MainImage").gameObject.SetActive(true);
            this.transform.Find("BoarderImage").gameObject.SetActive(true);
        }
    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            if (AssignedNPC == null)
            {
                AssignedNPC = isHeroPlace ? new Hero { Name = string.Empty } : new Monster { Name = string.Empty };
            }

            GameManager.Instance.chosenNPCPlaceScript = this;
            UIManager.Instance.OpenNPCPlaceWindow(AssignedNPC);
        }
    }

    public void SpawnNPC(NPC npc)
    {
        string folderPath = isHeroPlace ? "Prefabs/HeroesPrefabs" : "Prefabs/MonstersPrefabs";
        GameObject[] allNPCPrefabs = Resources.LoadAll<GameObject>(folderPath);

        GameObject npcPrefabToSpawn = null;

        foreach (GameObject npcPrefab in allNPCPrefabs)
        {
            NPC npcComponent = npcPrefab.GetComponent<NPC>();

            if (npcComponent != null && npcComponent.Name == npc.Name)
            {
                npcPrefabToSpawn = npcPrefab;
                break;
            }
        }

        if (npcPrefabToSpawn != null)
        {
            if (_spawnedNPC != null)
            {
                Destroy(_spawnedNPC);
            }

            Quaternion spawnRotation = isHeroPlace ? Quaternion.identity : Quaternion.Euler(0, -180, 0);
            _spawnedNPC = Instantiate(npcPrefabToSpawn, transform.position, spawnRotation);

            // Get Animator component for the spawned NPC
            _npcAnimator = _spawnedNPC.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError($"{(isHeroPlace ? "Hero" : "Monster")} prefab with name {npc.Name} not found in folder {folderPath}.");
        }
    }
    public void RemoveAssignedNPC()
    {
        Destroy(_spawnedNPC);
    }
}
