using UnityEngine;

public class CollectRewardsHandler : MonoBehaviour
{
    public LevelManager levelManager;

    void OnMouseDown()
    {
        if (levelManager != null)
        {
            levelManager.CollectRewards(); 
        }
    }
}
