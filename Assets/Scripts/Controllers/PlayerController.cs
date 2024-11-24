using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject objectsCollectorObject;
    private ObjectsCollector objectsCollector;

    private bool IsGateClosed;

    private void Start()
    {
        objectsCollector = objectsCollectorObject.GetComponent<ObjectsCollector>();

        IsGateClosed = true;
    }

    private void Update()
    {
        if (objectsCollector != null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (IsGateClosed) 
                {
                    objectsCollector.mainGateScript.OpenMainGate();
                    IsGateClosed = false;
                }
                else
                {
                    objectsCollector.mainGateScript.CloseMainGate();
                    IsGateClosed = true;
                }
            }

        }
    }
}
