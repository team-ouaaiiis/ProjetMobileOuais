using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class PlayerWeapon : Interactable
{
    #region Fields

    [Space(20)]
    public WeaponStats weapon;

    [Header("Weapon Movement")]
    public CurveTrajectory trajectory;
    [Range(0, 2)]
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
    [MinMaxSlider(0.0f,1.0f)] public Vector2 thrownHitboxActiveTime;
    float activeTimeMin, activeTimeMax;

    [Header("Power accumulation")]
    public AnimationCurve damageEvolution;
    public int maxThrown = 20;
    [ReadOnly] public int thrown = 0;
    public float timeBeforePowerOff = 1;
    float powerDecreaseTimer = 0;


    [Header("Weapon Attack values")]
    [SerializeField] [Range(0.0f, 1.0f)] float attackDelay = 0.0f;
    [SerializeField] [Range(0.0f, 1.0f)] float attackDuration = 0.2f;
    [SerializeField] LayerMask attackTargetLayer;
    [ReadOnly] public float currentDamage;
    [ReadOnly] public bool isAttacking;
    public UnityEvent onAttackStart;
    public UnityEvent onAttackEnd;
    IEnumerator attack;
    bool attackLaunched;

    //[Header("Debugs")]

    [BoxGroup("Debug Colors")]
    [SerializeField] bool showDebugBox = true;

    [ShowIf("showDebugBox")]
    [BoxGroup("Debug Colors")]
    [SerializeField] Color attackBoxColor = Color.red;

    [ShowIf("showDebugBox")]
    [BoxGroup("Debug Colors")]
    [SerializeField] Color throwSphereColor = Color.blue;

    [ShowIf("showDebugBox")]
    [BoxGroup("Debug Colors")]
    [SerializeField] Color curveActiveColor = Color.red;


    #endregion

    public override void Start()
    {
        base.Start();
        SetAttackBoxSize();
        ThrowMovement(0);
        currentDamage = GetThrownDamagePower();

        activeTimeMin = thrownHitboxActiveTime.x;
        activeTimeMax = thrownHitboxActiveTime.y;
    }

    public override void Update()
    {
        base.Update();
        ThrowTimer();

        if (isAttacking)
        {
            Attack();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!showDebugBox) return;
        SetAttackBoxSize();

        //alpha settings
        float boxAlpha = 0.4f;
        if (isAttacking) boxAlpha = 0.6f;

        //attackBoxSize box
        Gizmos.color = new Color(attackBoxColor.r, attackBoxColor.g, attackBoxColor.b, boxAlpha);
        Gizmos.DrawCube(transform.position + attackBoxOffset, attackBoxSize);
        Gizmos.color = new Color(attackBoxColor.r, attackBoxColor.g, attackBoxColor.b, 1);
        Gizmos.DrawWireCube(transform.position + attackBoxOffset, attackBoxSize);

        //attack box throw
        Gizmos.color = new Color(throwSphereColor.r, throwSphereColor.g, throwSphereColor.b, 0.4f);
        Gizmos.DrawSphere(transform.position, weapon.throwRadiusRange);
        Gizmos.color = new Color(throwSphereColor.r, throwSphereColor.g, throwSphereColor.b, 1);
        Gizmos.DrawWireSphere(transform.position, weapon.throwRadiusRange);

        //CURVE
        Gizmos.color = curveActiveColor;
        float min = thrownHitboxActiveTime.x;
        float max = thrownHitboxActiveTime.y;

        //The start position of the line
        Vector3 lastPos = trajectory.BezierCurvePoint(min);


        //The resolution of the line
        float resolution = 0.02f; //0.02 c'est bien, faut pas faire le fou avec cette variable


        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            float t = i * resolution;

            if(t < max && t > min)
            {
                Vector3 newPos = trajectory.BezierCurvePoint(t); //Find positions between the control points


                Gizmos.DrawLine(lastPos, newPos); //Draw as a new segment


                lastPos = newPos; //Save this pos pour draw the next segment
            }
        }
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
        if (!IsThrown)
        {

            if (thrown > 0)
            {
                powerDecreaseTimer += Time.deltaTime;

                if(powerDecreaseTimer >= timeBeforePowerOff)
                {
                    ResetThrownNumber();
                }
            }

            return;
        }

        if (weapon.reverse)
        {
            currentThrowTime -= Time.deltaTime;
            float percent = currentThrowTime / throwTime;
            ThrowMovement(percent);

            if (currentThrowTime <= 0)
            {
                EndThrow();
                weapon.reverse = false;
                SwitchDirection();
            }

            return;
        }

        currentThrowTime += Time.deltaTime;
        float timePercent = currentThrowTime / throwTime;
        ThrowMovement(timePercent);
        CheckHitbox(timePercent);

        if (currentThrowTime >= throwTime)
        {
            EndThrow();
        }
    }

    private void EndThrow()
    {
        IsThrown = false;
        currentThrowTime = 0; //reset timer
        SwitchDirection();
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
        ThrownNumberIncrement();

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
        float posT = speedEvolution.Evaluate(_timePercent);

        if (throwDirection == ThrowDirection.Left)
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

    void CheckHitbox(float _timePercent)
    {
        float percent = speedEvolution.Evaluate(_timePercent);

        if (percent > activeTimeMin && percent < activeTimeMax)
        {
            Debug.Log("ACTIVE");
            ThrowHitBox();
        }
    }

    void ThrowHitBox()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, weapon.throwRadiusRange, attackTargetLayer, QueryTriggerInteraction.Collide);
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

    void ThrownNumberIncrement()
    {
        thrown++;
        thrown = Mathf.Clamp(thrown, 0, maxThrown);
    }

    void ResetThrownNumber()
    {
        thrown = 0;
        powerDecreaseTimer = 0.0f;
    }

    public float GetThrownDamagePower()
    {
        float power = 0;

        float percent = (float)thrown / (float)maxThrown;
        float curveEval = damageEvolution.Evaluate(percent);
        power = Mathf.Lerp(weapon.damagePoint, weapon.maxDamagePoints, curveEval);

        return power;
    }

    public virtual void HitObstacle()
    {
        if(weapon.reverseMovement) weapon.reverse = true;

        if(weapon.cancelPower)
        {
            ResetThrownNumber();
            UpdateDamage();
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

        UpdateDamage();
        attack = Attacking();
        StartCoroutine(attack);
        ResetThrownNumber();
    }

    private void UpdateDamage()
    {
        currentDamage = GetThrownDamagePower();
    }

    public void CancelAttack()
    {
        if (attack != null)
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
                if (damageListener != null)
                {
                    damageListener.TakeDamage(weapon.damagePoint);
                    GameManager.instance.CallPlayerWeaponHitEntity();
                    Debug.Log("Damaging something");
                }
            }
        }        
    }

    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackDelay);
        isAttacking = true;
        onAttackStart.Invoke();
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
        onAttackEnd.Invoke();
    }

    #endregion
}
