using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float cameraMoveSpeed = 2f;
    public float cameraNPCPlaceOffset = -1.47f;
    public static CameraManager Instance { get; private set; }

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

    public void MoveCameraToPoint(Transform cameraPoint)
    {
        Camera mainCamera = Camera.main;

        StartCoroutine(MoveCamera(mainCamera, cameraPoint.position));
    }

    private IEnumerator MoveCamera(Camera camera, Vector3 targetPosition)
    {
        targetPosition.z = camera.transform.position.z;
        targetPosition.y += cameraNPCPlaceOffset;

        while (Vector3.Distance(camera.transform.position, targetPosition) > 0.1f)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
            yield return null;
        }

        camera.transform.position = targetPosition;
    }
}
