using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    public float damagePoint = 1;
    public bool isEquipped;
    public bool isThrown;
    [Header("Attack Range")]
    public float range = 1;
    public float throwRadiusRange = 1;
}

[System.Serializable]
public class Weapon
{
    public string weaponName = "weapon with no name";
    public PlayerWeapon playerWeapon;
    public bool weaponIsLocked;
    public GameObject weaponPrefab;
    public GameObject instance;

    public void GetPlayerWeaponInstance()
    {
        playerWeapon = instance.GetComponentInChildren<PlayerWeapon>();
    }

    public void ResetData()
    {
        playerWeapon = null;
        instance = null;
    }
}
