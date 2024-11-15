using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public NPCPlaceScript heroPlaceScript;
    public NPCPlaceScript monsterPlaceScript;

    public GameObject collectRewardsButton;

    public Player player;

    private bool isHeroTurn = true;
    public float attackDuration = 1.5f;  // Duration of attack animation
    public float hurtDuration = 1f;      // Duration of hurt animation
    public int coinsRewardPerSecond = 5;
    public int popularityRewardPerSecond = 2;

    private int totalCoins;  // Счетчик для накопленных монет
    private int totalPopularity;  // Счетчик для накопленной популярности

    void Start()
    {
        collectRewardsButton.SetActive(false);  // Скрыть кнопку до начала боя

        // Добавляем обработчик нажатия на collectRewardsButton
        AddClickHandlerToCollectButton();

        StartCoroutine(InitializeAndStartBattle());
    }

    // Добавляем обработчик клика
    void AddClickHandlerToCollectButton()
    {
        if (collectRewardsButton != null)
        {
            // Добавляем скрипт, который будет обрабатывать нажатия
            collectRewardsButton.AddComponent<CollectRewardsHandler>().levelManager = this;
        }
    }

    // Coroutine to initialize NPCs and start the battle
    IEnumerator InitializeAndStartBattle()
    {
        // Wait until both hero and monster have been assigned
        while (heroPlaceScript.AssignedNPC == null || monsterPlaceScript.AssignedNPC == null)
        {
            yield return null;  // Continue waiting if NPCs are not yet assigned
        }

        // Once NPCs are assigned, show the collect button and start the battle sequence
        collectRewardsButton.SetActive(true);
        StartCoroutine(BattleSequence());
        StartCoroutine(RewardAccumulation());
    }

    // Coroutine to manage alternating attacks
    IEnumerator BattleSequence()
    {
        while (heroPlaceScript.AssignedNPC != null && monsterPlaceScript.AssignedNPC != null)
        {
            if (isHeroTurn)
            {
                // Hero attacks, monster gets hurt
                heroPlaceScript.SetNPCState(isAttacking: true);  // Hero attacks
                monsterPlaceScript.SetHurtingState(isHurting: true);  // Monster gets hurt

                yield return new WaitForSeconds(attackDuration);  // Wait for the duration of attack
                heroPlaceScript.SetNPCState(isAttacking: false);  // End hero's attack
                monsterPlaceScript.SetHurtingState(isHurting: false);  // End monster's hurt state
            }
            else
            {
                // Monster attacks, hero gets hurt
                monsterPlaceScript.SetNPCState(isAttacking: true);  // Monster attacks
                heroPlaceScript.SetHurtingState(isHurting: true);  // Hero gets hurt

                yield return new WaitForSeconds(attackDuration);  // Wait for the duration of attack
                monsterPlaceScript.SetNPCState(isAttacking: false);  // End monster's attack
                heroPlaceScript.SetHurtingState(isHurting: false);  // End hero's hurt state
            }

            isHeroTurn = !isHeroTurn;  // Switch turn
            yield return new WaitForSeconds(hurtDuration);  // Short delay between attacks
        }
    }

    // Coroutine to accumulate rewards over time
    IEnumerator RewardAccumulation()
    {
        while (heroPlaceScript.AssignedNPC != null && monsterPlaceScript.AssignedNPC != null)
        {
            totalCoins += coinsRewardPerSecond;
            totalPopularity += popularityRewardPerSecond;

            yield return new WaitForSeconds(1f);  // Награда начисляется каждую секунду
        }
    }

    public void CollectRewards()
    {
        player.OnCoinsChange(totalCoins);
        player.OnPopularityChange(totalPopularity);

        // Сбрасываем счетчики
        totalCoins = 0;
        totalPopularity = 0;
    }
}
