using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private ObjectsCollector _objectsCollector;

    private GameObject _heroPlaceWindow;
    private GameObject _shopWindow;
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
        _shopWindow = _objectsCollector.ShopWindow;
    }

    public void OpenHeroPlaceWindow()
    {
        _heroPlaceWindow.SetActive(true);
    }

    public void OpenShopWindow()
    {
        _shopWindow.SetActive(true);
    }

    public void CloseShopWindow()
    {
        _shopWindow.SetActive(false);
    }
}
