using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTest : Obstacle
{
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);

        if(HealthPoints >= 0)
        {
            Player.playerInstance.weapon.HitObstacle();
            Debug.Log("Reverse Direction");
        }
        
    }

    public override void OnEnable()
    {
        base.OnEnable();
        HealthPoints = 1; //reset health
    }
}
