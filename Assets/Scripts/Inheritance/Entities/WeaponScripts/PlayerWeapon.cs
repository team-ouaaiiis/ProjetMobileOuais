using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Interactable
{

    public Weapon weapon;
    
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }


    public bool IsThrown
    {
        get
        {
            return weapon.isThrown;
        }
        set
        {
            if (value == true)
            {

            }
            else
            {

            }

            weapon.isThrown = value;
        }
    }


}
