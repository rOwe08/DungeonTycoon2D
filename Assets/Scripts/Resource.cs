public enum ResourceType
{
    Coins,
    Gems,
    Fear,
    Popularity,
    XP
}

public class Resource
{
    public ResourceType Type { get; private set; }
    public int Amount { get; private set; }

    public Resource(ResourceType type, int initialAmount)
    {
        Type = type;
        Amount = initialAmount;
    }

    public void ChangeAmount(int amountToAdd)
    {
        Amount += amountToAdd;
    }
}
