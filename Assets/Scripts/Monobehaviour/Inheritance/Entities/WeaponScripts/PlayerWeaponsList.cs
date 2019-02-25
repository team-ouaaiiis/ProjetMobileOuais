﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerWeaponsList : Manager
{
    [Header("Player's weapons")]
    public Weapon[] weapons;
    public int currentWeaponIndex;
    [ReadOnly]
    public Weapon currentWeapon;

    [Space(20)]
    [ReadOnly]
    public List<GameObject> playerWeaponInstances = new List<GameObject>();

    public static PlayerWeaponsList playerWeaponsList;

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        playerWeaponsList = this;
    }

    #endregion

    [Button]
    public void InstantiateAllWeapons()
    {
        if(playerWeaponInstances.Count > 0)
        {
            DestroyAllWeapons();
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            GameObject weapon = Instantiate(weapons[i].weaponPrefab, this.transform);
            playerWeaponInstances.Add(weapon);
            weapons[i].instance = weapon;
            weapons[i].GetPlayerWeaponInstance();
            weapon.SetActive(false);
        }
    }

    [Button]
    public void DestroyAllWeapons()
    {
        for (int i = 0; i < playerWeaponInstances.Count; i++)
        {
            DestroyImmediate(playerWeaponInstances[i]);
        }

        playerWeaponInstances = new List<GameObject>();

        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].ResetData();
        }

        currentWeapon = null;
    }

    [Button]
    public void LoadCurrentWeapon()
    {
        if (!weapons[currentWeaponIndex].weaponIsLocked)
        {
            playerWeaponInstances[currentWeaponIndex].SetActive(true);
            currentWeapon = weapons[currentWeaponIndex];
        }
        else
        {
            currentWeaponIndex = 0;
            LoadCurrentWeapon();
        }
    }

    [Button]
    public void DisableAllWeapons()
    {
        for (int i = 0; i < playerWeaponInstances.Count; i++)
        {
            playerWeaponInstances[i].SetActive(false);
        }
    }
}