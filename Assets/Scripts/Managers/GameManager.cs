using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public NPCPlaceScript chosenNPCPlaceScript;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AssignNPC(NPC npc)
    {
        chosenNPCPlaceScript.HandleNPCAssignment(npc);
    }

    public void DetachNPC()
    {
        chosenNPCPlaceScript.HandleNPCAssignment(null);
        chosenNPCPlaceScript.RemoveAssignedNPC();

        bool isHeroPlace = chosenNPCPlaceScript.isHeroPlace;

        NPC AssignedNPC = isHeroPlace ? new Hero { Name = string.Empty } : new Monster { Name = string.Empty };
        UIManager.Instance.UpdateNPCPlaceWindow(AssignedNPC);
    }
}
