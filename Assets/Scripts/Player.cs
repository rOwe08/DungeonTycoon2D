using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public List<Resource> resources;

    public int Level;
    public int XP;

    public List<Hero> HeroesOwned;
    public List<Monster> MonstersOwned;

    public List<Hero> HeroesOnPlaces;
    public List<Monster> MonstersOnPlaces;

    public UnityEvent OnResourcesChanged;

    private void Awake()
    {
        // TODO: LOAD SYSTEM
        resources = new List<Resource>()
        {
            new Resource(ResourceType.CoinsPerSecond, 0),    // Default 0 popularity
            new Resource(ResourceType.Coins, 100),       // Default 100 coins
            new Resource(ResourceType.Gems, 50),         // Default 50 gems
            new Resource(ResourceType.Fear, 0),          // Default 0 fear
            new Resource(ResourceType.Popularity, 0),    // Default 0 popularity
            new Resource(ResourceType.XP, 0)             // Default 0 XP
        };
    }

    public void OnResourceChange(ResourceType resourceType, int amountToAdd)
    {
        // Find the resource in the list
        Resource resource = resources.Find(r => r.Type == resourceType);

        if (resource != null)
        {
            resource.ChangeAmount(amountToAdd);
            OnResourcesChanged?.Invoke();
        }
        else
        {
            Debug.LogWarning("Resource not found: " + resourceType);
        }
    }

    // Wrapper methods for specific resources, if needed
    public void OnCoinsChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.Coins, amountToAdd);
    }

    public void OnGemsChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.Gems, amountToAdd);
    }

    public void OnFearChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.Fear, amountToAdd);
    }

    public void OnPopularityChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.Popularity, amountToAdd);
    }

    public void OnXPChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.XP, amountToAdd);
    }

    public void OnCoinsPerSecondChange(int amountToAdd)
    {
        OnResourceChange(ResourceType.CoinsPerSecond, amountToAdd);
    }

    public Resource GetResource(ResourceType resourceType)
    {
        Resource resource = resources.Find(r => r.Type == resourceType);
        return resource != null ? resource : null;
    }
}
