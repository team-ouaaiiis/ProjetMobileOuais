using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Enemy : Character, IDamageListener, IRepulseListener
{
    [BoxGroup("References"), SerializeField] Collider enemyCollider;

    [Header("Enemy values")]
    public float attackRange = 1;
    [SerializeField] Vector3 hitboxSize = Vector3.one;
    [SerializeField] public Vector3 hitboxOffset;
    [SerializeField] LayerMask playerTargetLayer;
    [SerializeField] LayerMask enemyLayer;

    [Header("Receive damage movement")]
    [SerializeField] float hitBackDistance = 1;
    [SerializeField] AnimationCurve movementCurve;
    [SerializeField] float movementTime = 0.5f;
    [ReadOnly] public bool onDamageMovement;
    float currentMoveTime;
    Vector3 iniPos;
    Vector3 targetPos;
    List<Collider> touchedColliders = new List<Collider>();

    [BoxGroup("Debug"), SerializeField] Color hitboxColor = Color.cyan;


    public override void Start()
    {
        base.Start();
        IniTouchedColliders();
    }
    public override void Update()
    {
        base.Update();
        ReceiveDamageMovement();
    }

    public virtual void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + hitboxOffset, hitboxSize / 2, Quaternion.identity, playerTargetLayer, QueryTriggerInteraction.Collide);

        if (colliders.Length > 0)
        {
            Debug.Log("Enemy touched something");
            for (int i = 0; i < colliders.Length; i++)
            {
                IDamageListener damageListener = colliders[i].GetComponent<IDamageListener>();
                if (damageListener != null)
                {
                    damageListener.TakeDamage(1);
                    Debug.Log("Damaging something");
                }
            }
        }
    }

    public void Repulsion()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + hitboxOffset, hitboxSize / 2, Quaternion.identity, enemyLayer, QueryTriggerInteraction.Collide);

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
                        Debug.Log("Repulse something");
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

    public void OnDrawGizmos()
    {
        Gizmos.color = hitboxColor;
        Gizmos.DrawWireCube(transform.position + hitboxOffset, hitboxSize);
        Gizmos.color = new Color(hitboxColor.r, hitboxColor.g, hitboxColor.b, 0.25f);
        Gizmos.DrawCube(transform.position + hitboxOffset, hitboxSize);
    }

    public override void Death()
    {
        base.Death();

        //DEBUG TEST
        gameObject.SetActive(false);
    }
}
