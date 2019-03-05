using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStandard : Enemy
{
    public override void Attack()
    {
        base.Attack();
    }

    public override void LateUpdate()
    {
        base.Update();
        Attack();
    }
}
