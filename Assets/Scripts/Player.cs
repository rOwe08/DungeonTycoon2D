using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int CoinsPerSecond;
    public int Coins;
    public int Gems;
    public int Fear;
    public int Popularity;

    public int Level;
    public int XP;

    public List<Hero> HeroesOwned;
    public List<Monster> MonstersOwned;


    private void Start()
    {
        HeroesOwned.Add(new Hero { Name = "Igor" });
        HeroesOwned.Add(new Hero { Name = "Kinannn" });
        HeroesOwned.Add(new Hero { Name = "Airat" });
    }
    public void OnCoinsPerSecondChange(int amountToAdd)
    {
        CoinsPerSecond += amountToAdd;
    }

    public void OnCoinsChange(int amountToAdd)
    {
        Coins += amountToAdd;
    }

    public void OnGemsChange(int amountToAdd)
    {
        Gems += amountToAdd;
    }

    public void OnFearChange(int amountToAdd)
    {
        Fear += amountToAdd;
    }

    public void OnPopularityChange(int amountToAdd)
    {
        Popularity += amountToAdd;
    }
}
