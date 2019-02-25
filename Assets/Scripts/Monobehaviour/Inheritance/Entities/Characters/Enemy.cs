using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamageListener
{
    public virtual void Attack()
    {

    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if(HealthPoints <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        base.Death();

        //DEBUG TEST
        gameObject.SetActive(false);
    }
}
