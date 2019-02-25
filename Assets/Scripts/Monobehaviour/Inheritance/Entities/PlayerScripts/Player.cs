using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region Fields

    [Header("Player's weapon")]
    public PlayerWeapon weapon;
    InputManager inputManager;


    #endregion

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        inputManager = InputManager.inputManager;
        inputManager.OnSwipe += OnSwipe;
        inputManager.OnTap += OnTap;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }

    #endregion

    #region Inputs Methods

    public void OnSwipe(InputManager.SwipeDirection _swipeDirection)
    {
        if (!weapon.IsThrown)
        {
            weapon.ThrowWeapon();
        }
    }

    public void OnTap()
    {
        if (!weapon.IsThrown)
        {
            weapon.StartAttack();
        }
    }

    #endregion
}
