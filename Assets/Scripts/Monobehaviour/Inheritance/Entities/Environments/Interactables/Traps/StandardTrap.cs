using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class StandardTrap : Trap
{

    public Projectile[] projectiles;


    public override void Awake()
    {
        base.Awake();

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        AttackProjectile();
    }

    [Button]
    void AttackProjectile()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            projectiles[i].StartAttack();
        }
    }



    


}
