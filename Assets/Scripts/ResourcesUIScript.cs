using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        CoinsPerSecondText.text = player.CoinsPerSecond.ToString();
        CoinsText.text = player.Coins.ToString(); ;
        GemsText.text = player.Gems.ToString(); ;
        FearText.text = player.Fear.ToString(); ;
        PopularityText.text = player.Popularity.ToString(); ;
    }
}
