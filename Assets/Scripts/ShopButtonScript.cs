using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonScript : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(UIManager.Instance.OpenShopWindow);
    }
}
