using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using SimpleJSON;
using System.IO;

public class WeaponData : SavedData
{
    [Header("Weapon data"), Space(10)]
    public bool[] weaponLock;
    public int equipedWeaponIndex;
    JSONObject weaponJson = new JSONObject();

    public static WeaponData instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void SaveData()
    {
        base.SaveData();

        //current weapon equiped index 
        weaponJson.Add("equipedWeaponIndex", equipedWeaponIndex);

        //lock weapon array
        JSONArray locks = new JSONArray();

        for (int i = 0; i < weaponLock.Length; i++)
        {
            locks.Add(weaponLock[i]);
        }

        weaponJson.Add("weaponLock", locks);

        //SAVE IN FILE
        File.WriteAllText(path, weaponJson.ToString());
        Debug.Log("SAVED DATA");
    }

    public override void LoadData()
    {
        base.LoadData();

        string jsonString = File.ReadAllText(path);
        weaponJson = (JSONObject)JSON.Parse(jsonString);

        equipedWeaponIndex = weaponJson["equipedWeaponIndex"];

        for (int i = 0; i < weaponLock.Length; i++)
        {
            weaponLock[i] = weaponJson["weaponLock"].AsArray[i];
        }

        Debug.Log("LOADED DATA");
    }

    public void UpdateData(Weapon[] _weaponlocks, int _equipedIndex)
    {
        for (int i = 0; i < _weaponlocks.Length; i++)
        {
            weaponLock[i] = _weaponlocks[i].weaponIsLocked;
        }

        equipedWeaponIndex = _equipedIndex;
    }
}
