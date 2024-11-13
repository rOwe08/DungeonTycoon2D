using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollector : MonoBehaviour
{
    [Header("Player Settings")]
    public Player Player;

    [Header("Gate Settings")]
    public GameObject MainGate;

    [Space(10)]
    [Header("NPC Place Settings")]
    public GameObject NPCPlaceTitleText;
    [Space(1)]
    public GameObject NPCPlaceWindow;
    public GameObject NPCAssignedPanel;
    public GameObject NPCAssignedNameText;
    public GameObject NPCPlaceVerticalLayoutPanel;

    [Space(10)]
    [Header("Shop Settings")]
    public GameObject ShopWindow;

    [Space(10)]
    [Header("Prefabs")]
    public GameObject OwndeNPCPlaceButton;

    [HideInInspector]
    public MainGateScript mainGateScript;


    // Start is called before the first frame update
    void Start()
    {
        mainGateScript = MainGate.GetComponent<MainGateScript>();
    }
}
