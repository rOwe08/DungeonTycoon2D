using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    public List<Hero> HeroesOnPlaces;
    public List<Monster> MonstersOnPlaces;

    public UnityEvent OnResourcesChanged;

    public void OnCoinsPerSecondChange(int amountToAdd)
    {
        CoinsPerSecond += amountToAdd;
        OnResourcesChanged?.Invoke();
    }

    public void OnCoinsChange(int amountToAdd)
    {
        Coins += amountToAdd;
        OnResourcesChanged?.Invoke();
    }

    public void OnGemsChange(int amountToAdd)
    {
        Gems += amountToAdd;
        OnResourcesChanged?.Invoke();
    }

    public void OnFearChange(int amountToAdd)
    {
        Fear += amountToAdd;
        OnResourcesChanged?.Invoke();
    }

    public void OnPopularityChange(int amountToAdd)
    {
        Popularity += amountToAdd;
        OnResourcesChanged?.Invoke();
    }
}
