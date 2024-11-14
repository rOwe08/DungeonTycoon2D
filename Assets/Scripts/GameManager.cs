using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public MonsterPlaceScript chosenMonsterPlaceScript;
    public HeroPlaceScript chosenHeroPlaceScript;

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
        if (npc is Hero hero)
        {
            chosenHeroPlaceScript.HandleNPCAssignment(npc);
        }
        else if (npc is Monster monster)
        {
            chosenMonsterPlaceScript.HandleNPCAssignment(npc);
        }
    }
}
