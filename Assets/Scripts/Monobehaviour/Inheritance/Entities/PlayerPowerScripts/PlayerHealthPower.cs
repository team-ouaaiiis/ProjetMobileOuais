using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthPower : PlayerPower
{
    [Header("Health power")]
    public float playerHealthPoints = 2;
    float iniPlayerHealth = 1;

    public override void ActivatePower()
    {
        base.ActivatePower();
        iniPlayerHealth = player.HealthPoints;
        player.HealthPoints = playerHealthPoints;
    }

    public override void DesactivatePower()
    {
        base.DesactivatePower();
        player.HealthPoints = iniPlayerHealth;
    }
}
