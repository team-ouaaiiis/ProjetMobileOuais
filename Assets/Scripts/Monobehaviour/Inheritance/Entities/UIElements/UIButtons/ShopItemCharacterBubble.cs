using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemCharacterBubble : UIButton
{
    [Header("Components")]
    [SerializeField] private Animator ItemCharacterBubbleAnimator;

    [Header("Settings")]
    [SerializeField] private float delaySpawn;
    [SerializeField] private int iD;

    private bool isSelected;

    public override void OnClick()
    {
        base.OnClick();
        UIManager.instance.CallShopBubbleSelection(iD);
    }

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
        isSelected = false;
    }

    public override void ShopBubbleSelection(int ID)
    {
        base.ShopBubbleSelection(ID);

        if (iD == ID && !isSelected)
        {
            isSelected = true;
            ItemCharacterBubbleAnimator.SetTrigger("Selection");
        }

        if (iD != ID && isSelected)
        {
            isSelected = false;
            ItemCharacterBubbleAnimator.SetTrigger("Deselection");
        }
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
