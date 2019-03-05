using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (HealthPoints <= 0 && !isDead)
        {
            Death();
        }
    }
}
