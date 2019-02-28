using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : Entity
{
    [HideInInspector]
    public Player player;

    public override void Start()
    {
        base.Start();
        player = Player.playerInstance;
        
    }

    public virtual void ActivatePower()
    {

    }

    public virtual void DesactivatePower()
    {

    }
}
