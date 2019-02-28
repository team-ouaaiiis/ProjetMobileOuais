using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Player : Character
{
    #region Fields

    [Header("Player's weapon")]
    [ReadOnly] public PlayerWeapon weapon;
    InputManager inputManager;
    PlayerWeaponsList weaponsList;

    [Header("Player's power")]
    public PlayerPower power;

    public static Player playerInstance;

    #endregion

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        playerInstance = this;
    }

    public override void Start()
    {
        base.Start();
        inputManager = InputManager.inputManager;
        inputManager.OnSwipe += OnSwipe;
        inputManager.OnTap += OnTap;

        //Get Weapon
        weaponsList = PlayerWeaponsList.playerWeaponsList;
        weaponsList.LoadCurrentWeapon();
        weapon = weaponsList.currentWeapon.playerWeapon;

        //Get Power
        if(power != null)
        {
            power.ActivatePower();
        }
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

    public override void Death()
    {
        base.Death();
        Debug.Log("Game over");
    }

    #region Inputs Methods

    public void OnSwipe(InputManager.SwipeDirection _swipeDirection)
    {
        if (!weapon.IsThrown)
        {
            weapon.ThrowWeapon();
        }
    }

    [Button("Tap")]
    public void OnTap()
    {
        if (!weapon.IsThrown)
        {
            weapon.StartAttack();
        }
    }

    [Button("Swipe")]
    void SwipeDebug()
    {
        OnSwipe(InputManager.SwipeDirection.Right);
    }

    #endregion
}
