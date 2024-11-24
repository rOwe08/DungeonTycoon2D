using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCOwnedButton : MonoBehaviour
{
    public NPC npcAttached;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeButton()
    {
        transform.Find("NPCOwnedNameText").GetComponent<TextMeshProUGUI>().text = npcAttached.Name;

        GetComponent<Button>().onClick.AddListener(() => { 
            GameManager.Instance.AssignNPC(npcAttached);
            UIManager.Instance.UpdateNPCPlaceWindow(npcAttached);
        });
    }
}
