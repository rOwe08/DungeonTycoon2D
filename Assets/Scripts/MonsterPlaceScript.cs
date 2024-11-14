using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterPlaceScript : MonoBehaviour
{
    public Monster MonsterAssigned;
    public GameObject CameraPoint;

    private GameObject _spawnedMonster;

    private void Start()
    {
        InputManager.Instance.OnTouchOrClickDetected.AddListener(HandleTouchOrClick);
    }

    private void OnDisable()
    {
        InputManager.Instance.OnTouchOrClickDetected.RemoveListener(HandleTouchOrClick);
    }

    public void HandleNPCAssignment(NPC npc)
    {
        if (npc is Monster monster)
        {
            MonsterAssigned = monster;
            SpawnMonster(MonsterAssigned);
        }

        if (MonsterAssigned != null)
        {
            this.transform.Find("MainImage").gameObject.SetActive(false);
            this.transform.Find("BoarderImage").gameObject.SetActive(false);
        }
        else
        {
            this.transform.Find("MainImage").gameObject.SetActive(true);
            this.transform.Find("BoarderImage").gameObject.SetActive(true);
        }
    }

    public void SpawnMonster(Monster monster)
    {
        GameObject[] allMonstersPrefabs = Resources.LoadAll<GameObject>("Prefabs/MonstersPrefabs");

        GameObject monsterPrefabToSpawn = null;

        foreach (GameObject monsterPrefab in allMonstersPrefabs)
        {
            Monster monsterComponent = monsterPrefab.GetComponent<Monster>();

            if (monsterComponent != null && monsterComponent.Name == monster.Name)
            {
                monsterPrefabToSpawn = monsterPrefab;
                break;
            }
        }

        if (monsterPrefabToSpawn != null)
        {
            if (_spawnedMonster != null)
            {
                Destroy(_spawnedMonster);
            }

            _spawnedMonster = Instantiate(monsterPrefabToSpawn, transform.position, Quaternion.Euler(0, -180, 0));
        }
        else
        {
            Debug.LogError("Monster prefab with name " + monster.Name + " not found in folder.");
        }

    }

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            Monster monsterAssigned = MonsterAssigned ?? new Monster { Name = string.Empty };

            if(MonsterAssigned is null)
            {
                MonsterAssigned = new Monster { Name = string.Empty };
            }

            GameManager.Instance.chosenMonsterPlaceScript = GetComponent<MonsterPlaceScript>();
            UIManager.Instance.OpenMonsterPlaceWindow(MonsterAssigned);

            //CameraManager.Instance.MoveCameraToPoint(CameraPoint.transform);
        }
    }
}
