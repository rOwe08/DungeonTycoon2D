using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceScript : MonoBehaviour
{
    public Hero HeroAssigned;
    private GameObject _spawnedHero;

    public GameObject CameraPoint;
    public Transform SpawnPoint;

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
        if (npc is Hero hero)
        {
            HeroAssigned = hero;
            SpawnHero(HeroAssigned);
        }

        if (HeroAssigned != null) 
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

    private void HandleTouchOrClick(Vector3 worldPosition)
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject)
        {
            if (HeroAssigned is null) 
            { 
                HeroAssigned = HeroAssigned ?? new Hero { Name = string.Empty };
            }

            GameManager.Instance.chosenHeroPlaceScript = GetComponent<HeroPlaceScript>();
            UIManager.Instance.OpenHeroPlaceWindow(HeroAssigned);

            // CameraManager.Instance.MoveCameraToPoint(CameraPoint.transform);
        }
    }

    public void SpawnHero(Hero hero)
    {
        GameObject[] allHeroPrefabs = Resources.LoadAll<GameObject>("Prefabs/HeroesPrefabs");

        GameObject heroPrefabToSpawn = null;

        foreach (GameObject heroPrefab in allHeroPrefabs)
        {
            Hero heroComponent = heroPrefab.GetComponent<Hero>();

            if (heroComponent != null && heroComponent.Name == hero.Name)
            {
                heroPrefabToSpawn = heroPrefab;
                break;
            }
        }

        if (heroPrefabToSpawn != null)
        {
            if (_spawnedHero is not null)
            {
                Destroy(_spawnedHero);
            }

            _spawnedHero = Instantiate(heroPrefabToSpawn, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Hero prefab with name " + hero.Name + " not found in folder.");
        }
    }
}
