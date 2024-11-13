using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ObjectsCollector _objectsCollector;

    private GameObject _heroPlaceWindow;

    public static UIManager Instance { get; private set; }

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

    private void Start()
    {
        _objectsCollector = FindAnyObjectByType<ObjectsCollector>();
        _heroPlaceWindow = _objectsCollector.HeroPlaceWindow;
    }

    public void OpenHeroPlaceWindow()
    {
        _heroPlaceWindow.SetActive(true);
    }
}
