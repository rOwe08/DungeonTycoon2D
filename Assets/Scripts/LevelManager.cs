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
    private bool isBattleInitialized = false;

    public int coinsRewardPerSecond = 5;
    public int popularityRewardPerSecond = 2;

    private int totalCoins;
    private int totalPopularity;

    void Start()
    {
        collectRewardsButton.SetActive(false);
        AddClickHandlerToCollectButton();
        StartCoroutine(InitializeAndStartBattle());
    }

    void AddClickHandlerToCollectButton()
    {
        if (collectRewardsButton != null)
        {
            collectRewardsButton.AddComponent<CollectRewardsHandler>().levelManager = this;
        }
    }

    IEnumerator InitializeAndStartBattle()
    {
        // Ждем назначения обоих NPC
        while (heroPlaceScript.AssignedNPC == null || monsterPlaceScript.AssignedNPC == null ||
               heroPlaceScript.npcAnimator == null || monsterPlaceScript.npcAnimator == null)
        {
            yield return null;
        }

        isBattleInitialized = true;

        // Показываем кнопку сбора наград и начинаем бой
        collectRewardsButton.SetActive(true);
        StartCoroutine(BattleSequence());
        StartCoroutine(RewardAccumulation());
    }

    IEnumerator BattleSequence()
    {
        while (heroPlaceScript.AssignedNPC != null && monsterPlaceScript.AssignedNPC != null)
        {
            if (isHeroTurn)
            {
                // Hero attacks, monster gets hurt
                yield return StartCoroutine(PlayAnimation(heroPlaceScript.npcAnimator, "atk", monsterPlaceScript.npcAnimator, "hurt"));
            }
            else
            {
                // Monster attacks, hero gets hurt
                yield return StartCoroutine(PlayAnimation(monsterPlaceScript.npcAnimator, "atk", heroPlaceScript.npcAnimator, "hurt"));
            }

            isHeroTurn = !isHeroTurn;  // Меняем ход
            yield return new WaitForSeconds(0.5f);  // Короткая задержка между ударами
        }
    }

    IEnumerator PlayAnimation(Animator attackerAnimator, string attackAnimPrefix, Animator receiverAnimator, string hurtAnimPrefix)
    {
        string attackAnimationName = GetAnimationName(attackerAnimator, attackAnimPrefix);
        string hurtAnimationName = GetAnimationName(receiverAnimator, hurtAnimPrefix);

        if (!string.IsNullOrEmpty(attackAnimationName) && !string.IsNullOrEmpty(hurtAnimationName))
        {
            // Отключаем зацикливание
            DisableAnimationLoop(attackerAnimator, attackAnimationName);
            DisableAnimationLoop(receiverAnimator, hurtAnimationName);

            // Запускаем анимации атаки и получения урона одновременно
            attackerAnimator.Play(attackAnimationName);
            receiverAnimator.Play(hurtAnimationName);

            // Определяем максимальную продолжительность между атакой и получением урона
            float attackDuration = GetAnimationDuration(attackerAnimator, attackAnimationName);
            float hurtDuration = GetAnimationDuration(receiverAnimator, hurtAnimationName);
            float maxDuration = Mathf.Max(attackDuration, hurtDuration);

            // Ждем завершения обеих анимаций
            yield return new WaitForSeconds(maxDuration);

            // Возвращаем аниматор в состояние "idle" после завершения анимаций
            attackerAnimator.Play("idle");
            receiverAnimator.Play("idle");
        }
    }

    float GetAnimationDuration(Animator animator, string animationName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;  // Возвращаем точную длину анимации
            }
        }
        return 1f;  // Если анимация не найдена, возвращаем 1 секунду по умолчанию
    }

    void DisableAnimationLoop(Animator animator, string animationName)
    {
        RuntimeAnimatorController controller = animator.runtimeAnimatorController;
        foreach (AnimationClip clip in controller.animationClips)
        {
            if (clip.name == animationName)
            {
                // Отключаем зацикливание
                clip.wrapMode = WrapMode.Once;
            }
        }
    }

    string GetAnimationName(Animator animator, string prefix)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name.Contains(prefix))
            {
                return clip.name;  // Возвращаем первое совпадение по префиксу
            }
        }
        return null;  // Если анимация не найдена
    }

    IEnumerator RewardAccumulation()
    {
        while (heroPlaceScript.AssignedNPC != null && monsterPlaceScript.AssignedNPC != null)
        {
            totalCoins += coinsRewardPerSecond;
            totalPopularity += popularityRewardPerSecond;

            yield return new WaitForSeconds(1f);
        }
    }

public void CollectRewards()
{
    StartCoroutine(PlayRewardAnimations());
}

IEnumerator PlayRewardAnimations()
{
    Animator rewardButtonAnimator = collectRewardsButton.GetComponent<Animator>();

    // Находим анимации с префиксами "h opening" и "h closing"
    string openingAnimation = GetAnimationName(rewardButtonAnimator, "h opening");
    string closingAnimation = GetAnimationName(rewardButtonAnimator, "h closing");

    if (!string.IsNullOrEmpty(openingAnimation) && !string.IsNullOrEmpty(closingAnimation))
    {
        // Запускаем анимацию "h opening"
        rewardButtonAnimator.Play(openingAnimation);
        yield return new WaitForSeconds(GetAnimationDuration(rewardButtonAnimator, openingAnimation));

        // Запускаем анимацию "h closing"
        rewardButtonAnimator.Play(closingAnimation);
        yield return new WaitForSeconds(GetAnimationDuration(rewardButtonAnimator, closingAnimation));

        // Применяем награды после завершения обеих анимаций
        player.OnCoinsChange(totalCoins);
        player.OnPopularityChange(totalPopularity);

        // Сбрасываем счетчики
        totalCoins = 0;
        totalPopularity = 0;
    }
}

}
