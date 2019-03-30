using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : PickableObject
{
    [Header("Coins values")]
    [SerializeField] float value;

    public override void GetObject()
    {
        base.GetObject();

    }
}
