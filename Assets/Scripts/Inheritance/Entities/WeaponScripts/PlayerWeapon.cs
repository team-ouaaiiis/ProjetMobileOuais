using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerWeapon : Interactable
{

    public Weapon weapon;

    [Header("Weapon Movement")]
    public CurveTrajectory trajectory;
    [Range(0,2)]
    [Tooltip("throw time before the weapon reach the other character")]
    [SerializeField] float throwTime = 0.2f; // le throw time before the weapon reach the other character
    float currentThrowTime = 0;

    public enum ThrowDirection
    {
        Right,
        Left
    }
    public ThrowDirection throwDirection;
    [SerializeField] AnimationCurve speedEvolution;

    
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        ThrowTimer();
    }

    #region Throw functions
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

    void ThrowTimer()
    {
        if (!IsThrown) return;

        currentThrowTime += Time.deltaTime;

        ThrowMovement(currentThrowTime / throwTime);

        if(currentThrowTime >= throwTime)
        {
            //end throw
            IsThrown = false;
            currentThrowTime = 0; //reset timer
            SwitchDirection();
        }
    }

    public void CancelThrow()
    {
        IsThrown = false;
        currentThrowTime = 0;
    }

    public void ThrowWeapon(ThrowDirection _throwDirection)
    {
        throwDirection = _throwDirection;
        IsThrown = true;

        switch (throwDirection)
        {
            case ThrowDirection.Right:
                break;
            case ThrowDirection.Left:
                break;
        }
    }

    [Button("Throw Weapon")]
    public void ThrowWeapon()
    {
        IsThrown = true;

        switch (throwDirection)
        {
            case ThrowDirection.Right:
                break;
            case ThrowDirection.Left:
                break;
        }
    }

    void ThrowMovement(float _timePercent)
    {
        float posT = 0;

        posT = speedEvolution.Evaluate(_timePercent);

        if(throwDirection == ThrowDirection.Left)
        {
            posT = 1 - posT;
        }

        transform.position = trajectory.BezierCurvePoint(posT);
    }

    void SwitchDirection()
    {
        switch (throwDirection)
        {
            case ThrowDirection.Right:
                throwDirection = ThrowDirection.Left;
                break;
            case ThrowDirection.Left:
                throwDirection = ThrowDirection.Right;
                break;
        }
    }

    #endregion
}
