using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacterSwapper : UIButton
{
    public override void OnClick()
    {
        base.OnClick();
        UIManager.instance.CallSwapCharacter();
    }
}
