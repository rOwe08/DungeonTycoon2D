using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroPlaceScript : MonoBehaviour
{
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DetectClickOrTouch(Input.mousePosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
        {
            DetectClickOrTouch(Input.GetTouch(0).position);
        }
    }

    void DetectClickOrTouch(Vector3 inputPosition)
    {
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(inputPosition);
        worldPosition.z = 0; 

        Collider2D hitCollider = Physics2D.OverlapPoint(worldPosition);
        if (hitCollider != null && hitCollider.gameObject == gameObject) 
        {
            UIManager.Instance.OpenHeroPlaceWindow();
        }
    }
}
