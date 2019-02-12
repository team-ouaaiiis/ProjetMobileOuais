using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Interactable
{

    public Weapon weapon;

    [Header("Movement")]
    public CurveTrajectory trajectory;
    [Range(0,2)]
    [Tooltip("throw time before the weapon reach the other character")]
    [SerializeField] float throwTime = 0.2f; // le throw time before the weapon reach the other character
    float currentThrowTime = 0;

    
    public override void Start()
    {
        base.Start();
    }

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
