using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesUIScript : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI CoinsPerSecondText;
    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI GemsText;
    public TextMeshProUGUI FearText;
    public TextMeshProUGUI PopularityText;


    private void Start()
    {
        UpdateResourcesPanel();
        player.OnResourcesChanged.AddListener(UpdateResourcesPanel);
    }

    public void UpdateResourcesPanel()
    {
        CoinsPerSecondText.text = player.GetResource(ResourceType.CoinsPerSecond).Amount.ToString();
        CoinsText.text = player.GetResource(ResourceType.Coins).Amount.ToString();
        GemsText.text = player.GetResource(ResourceType.Gems).Amount.ToString();
        FearText.text = player.GetResource(ResourceType.Fear).Amount.ToString();
        PopularityText.text = player.GetResource(ResourceType.Popularity).Amount.ToString();
    }
}
