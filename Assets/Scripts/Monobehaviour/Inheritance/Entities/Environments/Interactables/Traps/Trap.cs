using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Interactable
{
    public bool isUsed;

    public override void OnEnable()
    {
        base.OnEnable();
        isUsed = false;
    }

    public override void Death()
    {
        base.Death();
        isUsed = true;
    }
}
