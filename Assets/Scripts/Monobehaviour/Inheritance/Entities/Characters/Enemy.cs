using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Enemy : Character, IDamageListener, IRepulseListener
{
    [BoxGroup("References"), SerializeField] Collider enemyCollider;

    [Header("Enemy values")]
    public float attackRange = 1;
    [SerializeField] Vector3 damageBoxSize = Vector3.one;
    [SerializeField] public Vector3 damageBoxOffset;
    [SerializeField] LayerMask playerTargetLayer;
    [SerializeField] LayerMask enemyLayer;
    [ReadOnly] public bool canAttack = true;

    [Header("Receive damage movement")]
    [SerializeField] float hitBackDistance = 1;
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] float movementTime = 0.5f;
    [ReadOnly] public bool onDamageMovement;
    float currentMoveTime;
    Vector3 iniPos;
    Vector3 targetPos;
    List<Collider> touchedColliders = new List<Collider>();

    [BoxGroup("Debug"), SerializeField] Color damageBoxColor = Color.cyan;

    #region Monobehaviour callbacks

    public override void Start()
    {
        base.Start();
        IniTouchedColliders();
    }

    public override void Update()
    {
        base.Update();
        if(IsInitialized)
        {
            ReceiveDamageMovement();
        }
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        Attack();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        canAttack = true;
        ResetHealth();
    }

    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = damageBoxColor;
        Gizmos.DrawWireCube(transform.position + damageBoxOffset, damageBoxSize);
        Gizmos.color = new Color(damageBoxColor.r, damageBoxColor.g, damageBoxColor.b, 0.25f);
        Gizmos.DrawCube(transform.position + damageBoxOffset, damageBoxSize);
    }

    #region Public Methods

    public virtual void Attack()
    {
        if (!canAttack) return;

        Collider[] colliders = Physics.OverlapBox(transform.position + damageBoxOffset, damageBoxSize / 2, Quaternion.identity, playerTargetLayer, QueryTriggerInteraction.Collide);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                IDamageListener damageListener = colliders[i].GetComponent<IDamageListener>();
                if (damageListener != null)
                {
                    damageListener.TakeDamage(1);
                    canAttack = false;
                }
            }
        }
    }

    public void Repulsion()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + damageBoxOffset, damageBoxSize / 2, Quaternion.identity, enemyLayer, QueryTriggerInteraction.Collide);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (!touchedColliders.Contains(colliders[i]))
                {
                    touchedColliders.Add(colliders[i]);
                    IRepulseListener repulseListener = colliders[i].GetComponent<IRepulseListener>();
                    if (repulseListener != null)
                    {
                        repulseListener.Repulse();
                    }
                }
            }
        }
    }

    public void Repulse()
    {
        StartHitMovement();
    }

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
        Repulse();
    }

    [Button]
    public void StartHitMovement()
    {
        targetPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + hitBackDistance);
        iniPos = transform.localPosition;
        onDamageMovement = true;
    }

    public virtual void ReceiveDamageMovement()
    {
        if (!onDamageMovement) return;

        currentMoveTime += Time.deltaTime;
        float percent = currentMoveTime / movementTime;
        float t = movementCurve.Evaluate(percent);

        transform.localPosition = Vector3.Lerp(iniPos, targetPos, t);
        Repulsion();

        if(currentMoveTime >= movementTime)
        {
            currentMoveTime = 0.0f;
            onDamageMovement = false;
            IniTouchedColliders();
        }
    }

    private void IniTouchedColliders()
    {
        touchedColliders = new List<Collider>(); //reset list
        touchedColliders.Add(enemyCollider);
    }

    public override void Death()
    {
        base.Death();
        canAttack = false;
        //Debug.Log(gameObject.name + " DIED", gameObject);
        //DEBUG TEST
        gameObject.SetActive(false);
        GameManager.instance.DebugParticle();
        transform.parent = Holder;
    }    

    #endregion
}
