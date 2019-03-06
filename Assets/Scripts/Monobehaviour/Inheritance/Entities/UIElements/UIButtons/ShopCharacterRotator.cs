using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacterRotator : UIButton
{
    [Header("Components")]
    [SerializeField] private Animator CharacterRotatorAnimator;

    public override void OnClick()
    {
        base.OnClick();
        CharacterRotatorAnimator.SetBool("isSwitch1", !CharacterRotatorAnimator.GetBool("isSwitch1"));
    }
}
