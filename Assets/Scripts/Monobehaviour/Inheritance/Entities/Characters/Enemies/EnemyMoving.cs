using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : Enemy
{
    [Header("Enemy moving values")]
    public bool canMove;
    public float speed = 20;


    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
        EnemyMove();
    }

    #endregion

    public virtual void EnemyMove()
    {
        if (!canMove) return;

        transform.Translate(Vector3.back * speed * Time.deltaTime, UnityEngine.Space.Self);
    }
}
