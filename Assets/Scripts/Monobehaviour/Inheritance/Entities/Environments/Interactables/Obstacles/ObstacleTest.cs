using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObstacleTest : Obstacle
{

    public UnityEvent onDestroy;

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if(HealthPoints >= 0)
        {
            Player.playerInstance.weapon.HitObstacle();
            Debug.Log("Reverse Direction");
            onDestroy.Invoke();
        }
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        HealthPoints = 1; //reset health
    }
}
