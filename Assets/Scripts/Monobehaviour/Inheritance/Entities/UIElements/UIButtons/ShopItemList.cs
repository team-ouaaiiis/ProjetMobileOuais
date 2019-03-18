using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemList : Entity
{
    [Header("Components")]
    [SerializeField] private Animator ShopItemListAnimator;

    private bool isOn;

    public override void ShopBubbleSelection(int ID)
    {
        if (!isOn)
        {
            ShopItemListAnimator.SetBool("Spawn", true);
            isOn = true;
        }
    }

    public override void ShopBubbleDespawn()
    {
        if (isOn)
        {
            ShopItemListAnimator.SetBool("Spawn", false);
            isOn = false;
        }
    }
}
