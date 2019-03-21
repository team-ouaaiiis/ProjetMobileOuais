using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField] private Animator anim;

    public override void OnDisable()
    {
        base.OnDisable();
        //Debug.Log(gameObject.name + " was Disabled", gameObject);
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if (HealthPoints <= 0 && !isDead)
        {
            Death();
        }
    }

    public Animator Anim { get => anim; }

}
