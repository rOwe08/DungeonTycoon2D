using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public GameObject Decorations;

    public NPCPlaceScript heroPlaceScript;
    public NPCPlaceScript monsterPlaceScript;

    public GameObject upgradeLevelAreaObject;

    public GameObject collectRewardsButton;

    public Player player;

    private bool isHeroTurn = true;
    private bool isBattleInitialized = false;

    public int coinsRewardPerSecond = 5;
    public int popularityRewardPerSecond = 2;

    private int totalCoins;
    private int totalPopularity;

    public int level = 0;
    public int costForNextLevel = 100;

    public GameObject coinIconPrefab;  
    public GameObject popularityIconPrefab; 

    public Transform coinsTargetIcon;
    public Transform popularityTargetIcon;

    void Start()
    {
        collectRewardsButton.SetActive(false);
        AddClickHandlerToCollectButton();
        StartCoroutine(InitializeAndStartBattle());
    }

    public void UpLevel()
    {
        // TODO: MAX LEVEL REACHED
        
        level++;
        costForNextLevel += 50;

        Debug.Log("Upgraded to level: " + level);

        if (level % 10 == 0)
        {
            ActivateDecoration(level);
        }
    }

    void ActivateDecoration(int level)
    {
        Transform decoration = Decorations.transform.Find(level.ToString());

        if (decoration != null)
        {
            decoration.gameObject.SetActive(true);
            Debug.Log("Activated decoration for level " + level);
        }
        else
        {
            Debug.LogWarning("Decoration for level " + level + " not found!");
        }
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

        string openingAnimation = GetAnimationName(rewardButtonAnimator, "h opening");
        string closingAnimation = GetAnimationName(rewardButtonAnimator, "h closing");

        if (!string.IsNullOrEmpty(openingAnimation) && !string.IsNullOrEmpty(closingAnimation))
        {
            rewardButtonAnimator.Play(openingAnimation);
            yield return new WaitForSeconds(GetAnimationDuration(rewardButtonAnimator, openingAnimation));

            rewardButtonAnimator.Play(closingAnimation);
            yield return new WaitForSeconds(GetAnimationDuration(rewardButtonAnimator, closingAnimation));

            // Применяем награды
            player.OnCoinsChange(totalCoins);
            player.OnPopularityChange(totalPopularity);

            // Создаем анимацию для иконок наград через DOTween
            AnimateRewardIcons(totalCoins, coinsTargetIcon, coinIconPrefab);
            AnimateRewardIcons(totalPopularity, popularityTargetIcon, popularityIconPrefab);

            totalCoins = 0;
            totalPopularity = 0;
        }
    }

    void AnimateRewardIcons(int amount, Transform target, GameObject iconPrefab)
    {
        for (int i = 0; i < amount; i++)
        {
            // Создаем иконку ресурса на экране
            GameObject icon = Instantiate(iconPrefab, collectRewardsButton.transform.position, Quaternion.identity, collectRewardsButton.transform);

            // Делаем плавное движение иконки к цели с помощью DOTween
            icon.transform.DOMove(target.position, 1f)
                .SetEase(Ease.InOutQuad)     // Добавляем плавное ускорение/замедление
                .OnComplete(() => Destroy(icon));  // Удаляем иконку после завершения анимации

            // Дополнительно можно добавить анимацию прозрачности
            icon.GetComponent<CanvasGroup>().DOFade(0, 1f);

            // Интервал между появлением иконок
            DOTween.Sequence().AppendInterval(0.1f);
        }
    }

}
