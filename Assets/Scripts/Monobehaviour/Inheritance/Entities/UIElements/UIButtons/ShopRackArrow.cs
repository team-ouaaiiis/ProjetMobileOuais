using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRackArrow : UIButton
{
    [SerializeField] private bool isRight;

    public override void OnClick()
    {
        UIManager.instance.CallShopRackMove(isRight);
    }

}
