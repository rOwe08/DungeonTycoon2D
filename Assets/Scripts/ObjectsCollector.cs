using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollector : MonoBehaviour
{

    public GameObject MainGate;

    [HideInInspector]
    public MainGateScript mainGateScript;


    // Start is called before the first frame update
    void Start()
    {
        mainGateScript = MainGate.GetComponent<MainGateScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
