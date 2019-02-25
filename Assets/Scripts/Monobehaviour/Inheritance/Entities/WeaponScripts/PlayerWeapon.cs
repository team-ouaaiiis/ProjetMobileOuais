using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerWeapon : Interactable
{
    #region Fields

    [Space(20)]
    public WeaponStats weapon;

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

    [Header("Weapon Attack range")]
    [SerializeField] Vector3 attackBoxOffset;
    Vector3 attackBoxSize = Vector3.one;

    [Header("Weapon Attack values")]
    [ReadOnly] public bool isAttacking;
    [SerializeField] [Range(0.0f,1.0f)] float attackDelay = 0.0f;
    [SerializeField] [Range(0.0f,1.0f)] float attackDuration = 0.2f;
    [SerializeField] LayerMask attackTargetLayer;
    IEnumerator attack;

    //[Header("Debugs")]

    [BoxGroup("Debug Colors")]
    [SerializeField] bool showDebugBox = true;

    [ShowIf("showDebugBox")]
    [BoxGroup("Debug Colors")]
    [SerializeField] Color attackBoxColor = Color.red;

    [ShowIf("showDebugBox")]
    [BoxGroup("Debug Colors")]
    [SerializeField] Color throwSphereColor = Color.blue;


    #endregion

    public override void Start()
    {
        base.Start();
        SetAttackBoxSize();
        ThrowMovement(0);
    }

    public override void Update()
    {
        base.Update();
        ThrowTimer();
    }

    private void OnDrawGizmosSelected()
    {
        if (!showDebugBox) return;
        SetAttackBoxSize();

        //alpha settings
        float boxAlpha = 0.4f;
        if (isAttacking) boxAlpha = 0.6f;

        //attackBoxSize box
        Gizmos.color = new Color(attackBoxColor.r, attackBoxColor.g,attackBoxColor.b,boxAlpha);
        Gizmos.DrawCube(transform.position + attackBoxOffset, attackBoxSize);
        Gizmos.color = new Color(attackBoxColor.r, attackBoxColor.g, attackBoxColor.b, 1);
        Gizmos.DrawWireCube(transform.position + attackBoxOffset, attackBoxSize);

        //attack box throw
        Gizmos.color = new Color(throwSphereColor.r, throwSphereColor.g, throwSphereColor.b, 0.4f);
        Gizmos.DrawSphere(transform.position, weapon.throwRadiusRange);
        Gizmos.color = new Color(throwSphereColor.r, throwSphereColor.g, throwSphereColor.b, 1);
        Gizmos.DrawWireSphere(transform.position, weapon.throwRadiusRange);
    }

    void SetAttackBoxSize()
    {
        attackBoxSize = new Vector3(attackBoxSize.x, attackBoxSize.y, weapon.range);
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
        ThrowHitBox();

        if(currentThrowTime >= throwTime)
        {
            //end throw
            IsThrown = false;
            currentThrowTime = 0; //reset timer
            SwitchDirection();
        }
    }

    /// <summary>
    /// Cancel the throw movement of the weapon 
    /// </summary>
    public void CancelThrow()
    {
        IsThrown = false;
        currentThrowTime = 0;
    }

    /// <summary>
    /// start Throwing weapon in a choosen direction
    /// </summary>
    /// <param name="_throwDirection"></param>
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

    /// <summary>
    /// Start throwing weapon
    /// </summary>
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

    void ThrowHitBox()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,weapon.throwRadiusRange,attackTargetLayer,QueryTriggerInteraction.Collide);
        Debug.Log("THROW ATTACK");

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                IDamageListener damageListener = colliders[i].GetComponent<IDamageListener>();
                if (damageListener != null)
                {
                    damageListener.TakeDamage(weapon.damagePoint);
                    Debug.Log("Damaging something with throw attack");
                }
            }
        }
    }

    #endregion

    #region Attack functions

    [Button]
    public void StartAttack()
    {
        if (isAttacking)
        {
            Debug.LogWarning("Already attacking");
            return;
        }

        attack = Attacking();
        StartCoroutine(attack);
        isAttacking = true;
    }

    public void CancelAttack()
    {
        if(attack != null)
        {
            StopCoroutine(attack);
        }

        isAttacking = false;
    }

    void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + attackBoxOffset, attackBoxSize / 2, Quaternion.identity, attackTargetLayer, QueryTriggerInteraction.Collide);
        Debug.Log("ATTACKING");

        if (colliders.Length > 0)
        {
            Debug.Log("Somehting detected");
            for (int i = 0; i < colliders.Length; i++)
            {
                IDamageListener damageListener = colliders[i].GetComponent<IDamageListener>();
                if(damageListener != null)
                {
                    damageListener.TakeDamage(weapon.damagePoint);
                    Debug.Log("Damaging something");
                }
            }
        }
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackDelay);
        Attack();
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    #endregion
}
