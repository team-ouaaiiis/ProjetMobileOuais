using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoins : Entity
{
    [SerializeField] int coins = 0;
    public static PlayerCoins instance;

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void OnGameOver()
    {
        base.OnGameOver();
        SaveCoins();
    }

    #endregion

    #region public methods
    public int Coins { get => coins; }

    public void AddCoins(int amountToAdd)
    {
        coins += amountToAdd;
        Debug.Log("ADD COINS");
    }

    public void SaveCoins()
    {

    }

    #endregion
}
