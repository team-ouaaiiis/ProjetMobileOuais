using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerPowerList : Manager
{
    public static PlayerPowerList powerListInstance;

    public PlayerPower[] playerPowers;
    [ReadOnly] public PlayerPower currentPower;
    [SerializeField] int playerPowerIndex;

    public int PlayerPowerIndex
    {
        get
        {
            return playerPowerIndex;
        }
        set
        {
            playerPowerIndex = value;
            SetCurrentPower();
        }
    }

    public override void Awake()
    {
        base.Start();
        PlayerPowerIndex = playerPowerIndex;
        powerListInstance = this;
    }

    public void SetCurrentPower()
    {
        if(playerPowerIndex > playerPowers.Length - 1)
        {
            Debug.LogError("L'INDEX DE POWER EST TROP HAUT");
            playerPowerIndex = 0;
        }

        currentPower = playerPowers[playerPowerIndex];
    }

    public PlayerPower GetCurrentPower()
    {
        SetCurrentPower();
        return currentPower;
    }
}
