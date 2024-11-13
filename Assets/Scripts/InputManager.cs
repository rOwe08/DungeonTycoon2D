using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    private bool _touchHandled;
    private Camera _mainCamera;

    public UnityEvent<Vector3> OnTouchOrClickDetected;

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

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            _touchHandled = true;
            DetectClickOrTouch(Input.GetTouch(0).position);
        }

        if (Input.GetMouseButtonDown(0) && !_touchHandled)
        {
            DetectClickOrTouch(Input.mousePosition);
        }

        if (Input.touchCount == 0)
        {
            _touchHandled = false;
        }
    }

    private void DetectClickOrTouch(Vector3 inputPosition)
    {
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(inputPosition);
        worldPosition.z = 0;

        OnTouchOrClickDetected?.Invoke(worldPosition);
    }
}
