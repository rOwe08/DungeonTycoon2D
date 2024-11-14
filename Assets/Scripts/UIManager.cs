using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ObjectsCollector _objectsCollector;

    private GameObject _npcPlaceTitleText;

    private GameObject _npcPlaceWindow;
    private GameObject _npcAssignedPanel;
    private GameObject _npcAssignedNameText;

    private GameObject _npcPlaceVerticalLayoutPanel;
    private GameObject _ownedNpcButtonPrefab;

    private GameObject _monsterPlaceWindow;

    private GameObject _shopWindow;

    private List<GameObject> _npcPlacedButtons;

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

        _npcPlaceVerticalLayoutPanel = _objectsCollector.NPCPlaceVerticalLayoutPanel;
        _ownedNpcButtonPrefab = _objectsCollector.OwndeNpcPlaceButton;

        _shopWindow = _objectsCollector.ShopWindow;

        _npcPlacedButtons = new List<GameObject>();
    }

    public void UpdateNPCPlaceWindow(NPC npcAssigned)
    {
        foreach(GameObject button in _npcPlacedButtons)
        {
            Destroy(button);
        }
        _npcPlacedButtons.Clear();
        List<NPC> npcList = new List<NPC>();

        if (npcAssigned.GetType() == typeof(Hero))
        {
            _npcPlaceTitleText.GetComponent<TextMeshProUGUI>().text = "HERO";
            npcList = _objectsCollector.Player.HeroesOwned.Cast<NPC>().ToList();
            // - heroes on places

        }
        else if (npcAssigned.GetType() == typeof(Monster))
        {
            _npcPlaceTitleText.GetComponent<TextMeshProUGUI>().text = "MONSTER";
            npcList = _objectsCollector.Player.MonstersOwned.Cast<NPC>().ToList();
            // - monsters on places
        }

        foreach (NPC npcToShow in npcList)
        {
            GameObject npcButton = Instantiate(_ownedNpcButtonPrefab, _npcPlaceVerticalLayoutPanel.transform);
            _npcPlacedButtons.Add(npcButton);

            npcButton.GetComponent<NPCOwnedButton>().npcAttached = npcToShow;
            npcButton.GetComponent<NPCOwnedButton>().InitializeButton();
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

    public void OpenNPCPlaceWindow(NPC npcAssigned)
    {
        UpdateNPCPlaceWindow(npcAssigned);
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
