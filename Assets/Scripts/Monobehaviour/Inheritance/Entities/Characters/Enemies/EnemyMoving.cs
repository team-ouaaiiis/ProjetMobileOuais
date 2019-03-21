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
        if(IsInitialized)
            EnemyMove();
    }

    #endregion

    public virtual void EnemyMove()
    {
        if (!canMove) return;

        //transform.Translate(Vector3.forward * speed * Time.deltaTime, UnityEngine.Space.Self);
    }
}
