using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _mainCamera;

    public float moveSpeed = 5f;    
    public float dragSpeed = 0.1f;  

    private float minY = -8f;       
    private float maxY = 0f;      

    private Vector3 _dragOrigin;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        HandleKeyboardInput();

        HandleDrag();
    }

    void HandleKeyboardInput()
    {
        float verticalMovement = 0;

        if (Input.GetKey(KeyCode.W)) 
        {
            verticalMovement = moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalMovement = -moveSpeed * Time.deltaTime;
        }

        Vector3 position = _mainCamera.transform.position;
        position.y = Mathf.Clamp(position.y + verticalMovement, minY, maxY);
        _mainCamera.transform.position = position;
    }

    void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragOrigin = Input.mousePosition; 
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 difference = _dragOrigin - currentMousePos;
            _dragOrigin = currentMousePos;

            Vector3 position = _mainCamera.transform.position;
            position.y = Mathf.Clamp(position.y + difference.y * dragSpeed * Time.deltaTime, minY, maxY);
            _mainCamera.transform.position = position;
        }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;

            Vector3 position = _mainCamera.transform.position;
            position.y = Mathf.Clamp(position.y - touchDeltaPosition.y * dragSpeed * Time.deltaTime, minY, maxY); 
            _mainCamera.transform.position = position;
        }
    }
}
