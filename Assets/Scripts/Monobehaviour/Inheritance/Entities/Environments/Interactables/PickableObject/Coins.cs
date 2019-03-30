using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : PickableObject
{
    [Header("Coins values")]
    [SerializeField] int value;

    public override void GetObject()
    {
        base.GetObject();
        PlayerCoins.instance.AddCoins(value);
        gameObject.SetActive(false);
    }
}
