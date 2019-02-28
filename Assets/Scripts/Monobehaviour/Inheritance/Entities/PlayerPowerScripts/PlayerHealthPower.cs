using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPower : PlayerPower
{
    [Header("Health power")]
    public float playerHealthPoints = 2;
    float iniHealthPoints = 1;

    public override void ActivatePower()
    {
        base.ActivatePower();
        iniHealthPoints = player.HealthPoints;
        player.HealthPoints = playerHealthPoints;
    }

    public override void DesactivatePower()
    {
        base.DesactivatePower();
        player.HealthPoints = iniHealthPoints;
    }
}
