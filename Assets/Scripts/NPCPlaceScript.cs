using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPlaceScript : MonoBehaviour
{
    public NPC AssignedNPC;
    public GameObject CameraPoint;
    private GameObject _spawnedNPC;
    public Transform SpawnPoint;

    private bool isHeroPlace;

    private void Start()
    {
        if (gameObject.name.ToLower().Contains("hero"))
        {
            isHeroPlace = true;
        }
        else if (gameObject.name.ToLower().Contains("monster"))
        {
            isHeroPlace = false;
        }
        else
        {
            Debug.LogError("NPCPlaceScript: Object name should contain 'Hero' or 'Monster'.");
            return;
        }

        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
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
        }
        else
        {
            Debug.LogError($"{(isHeroPlace ? "Hero" : "Monster")} prefab with name {npc.Name} not found in folder {folderPath}.");
        }
    }
}
