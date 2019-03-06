using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCharacterBubble : UIButton
{
    [Header("Components")]
    [SerializeField] private Animator ItemCharacterBubbleAnimator;

    [Header("Settings")]
    [SerializeField] private float delaySpawn;

    public override void ShopBubbleSpawn()
    {
        base.SwapCharacter();
        StopAllCoroutines();
        StartCoroutine(WaitAndSpawn());
    }

    public override void ShopBubbleDespawn()
    {
        base.SwapCharacter();
        StopAllCoroutines();
        StartCoroutine(WaitAndDespawn());
    }

    private IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSecondsRealtime(delaySpawn);
        ItemCharacterBubbleAnimator.SetTrigger("Spawn");
    }

    private IEnumerator WaitAndDespawn()
    {
        yield return new WaitForSecondsRealtime(delaySpawn);
        ItemCharacterBubbleAnimator.SetTrigger("Despawn");
    }
}
