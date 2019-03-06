using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacterSelector : UIButton
{
    [SerializeField] private bool isOn;
    private bool isActive;

    public override void Start()
    {
        base.Start();
        isActive = isOn;
    }

    public override void OnClick()
    {
        if (isActive)
        {
            base.OnClick();
            UIManager.instance.CallShopBubbleSpawn(isOn);
            isActive = false;
        }
    }

    public override void ShopBubbleSpawn()
    {
        if (!isOn)
            isActive = true;
    }

    public override void ShopBubbleDespawn()
    {
        if (isOn)
            isActive = true;
    }
}
