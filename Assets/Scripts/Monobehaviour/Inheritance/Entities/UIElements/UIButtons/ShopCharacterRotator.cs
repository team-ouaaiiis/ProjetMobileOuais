using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCharacterRotator : Entity
{
    [Header("Components")]
    [SerializeField] private Animator CharacterRotatorAnimator;

    public override void SwapCharacter()
    {
        base.SwapCharacter();
        CharacterRotatorAnimator.SetBool("isSwitch1", !CharacterRotatorAnimator.GetBool("isSwitch1"));
    }
}
