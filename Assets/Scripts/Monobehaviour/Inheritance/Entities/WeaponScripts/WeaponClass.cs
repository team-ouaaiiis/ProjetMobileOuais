﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public float damagePoint = 1;
    public bool isEquipped;
    public bool isThrown;
    [Header("Attack Range")]
    public float range = 1;
    public float throwRadiusRange = 1;
}