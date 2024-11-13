using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ObjectsCollector _objectsCollector;

    private GameObject _npcPlaceTitleText;

    private GameObject _npcPlaceWindow;
    private GameObject _npcAssignedPanel;
    private GameObject _npcAssignedNameText;

    private GameObject _monsterPlaceWindow;

    private GameObject _shopWindow;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _objectsCollector = FindAnyObjectByType<ObjectsCollector>();

        _npcPlaceTitleText = _objectsCollector.NPCPlaceTitleText;

        _npcPlaceWindow = _objectsCollector.NPCPlaceWindow;
        _npcAssignedPanel = _objectsCollector.NPCAssignedPanel;
        _npcAssignedNameText = _objectsCollector.NPCAssignedNameText;

        _shopWindow = _objectsCollector.ShopWindow;
    }

    private void PrepareNPCPlaceWindow(NPC npcAssigned)
    {
        if (npcAssigned.GetType() == typeof(Hero))
        {
            _npcPlaceTitleText.GetComponent<TextMeshProUGUI>().text = "HERO";
        }
        else if (npcAssigned.GetType() == typeof(Monster))
        {
            _npcPlaceTitleText.GetComponent<TextMeshProUGUI>().text = "MONSTER";
        }

        if (string.IsNullOrEmpty(npcAssigned.Name))
        {
            if (npcAssigned.GetType() == typeof(Hero))
            {
                _npcAssignedNameText.GetComponent<TextMeshProUGUI>().text = "NO HERO ASSIGNED";
            }
            else if (npcAssigned.GetType() == typeof(Monster))
            {
                _npcAssignedNameText.GetComponent<TextMeshProUGUI>().text = "NO MONSTER ASSIGNED";
            }
        }
        else
        {
            _npcAssignedNameText.GetComponent<TextMeshProUGUI>().text = npcAssigned.Name;
        }
    }


    public void OpenHeroPlaceWindow(Hero heroAssigned)
    {
        PrepareNPCPlaceWindow(heroAssigned);
        _npcPlaceWindow.SetActive(true);
    }

    public void OpenMonsterPlaceWindow(Monster monsterAssigned)
    {
        PrepareNPCPlaceWindow(monsterAssigned);
        _npcPlaceWindow.SetActive(true);
    }

    public void OpenShopWindow()
    {
        _shopWindow.SetActive(true);
    }

    public void CloseShopWindow()
    {
        _shopWindow.SetActive(false);
    }
}
