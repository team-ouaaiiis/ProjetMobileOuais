using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : Environment
{
    public override void OnEnable()
    {
        base.OnEnable();
        ResetHealth();
    }
}
