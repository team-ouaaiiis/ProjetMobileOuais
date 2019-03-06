using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Projectile : Interactable
{
    [Header("Projectiles")]
    [ReadOnly] public bool moveProjectile;
    [SerializeField] float projectileSpeed = 5;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] Vector3 moveDirection = Vector3.left;
    Vector3 iniPosition;

    [Header("Damage zone")]
    [ReadOnly, SerializeField] bool canAttack;
    [SerializeField] Vector3 damageBoxSize;
    [SerializeField] Vector3 damageBoxOffset;

    [BoxGroup("Debug"), SerializeField] Color damageBoxColor = Color.cyan;

    public override void Awake()
    {
        base.Awake();
        iniPosition = transform.localPosition;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = damageBoxColor;
        Gizmos.DrawWireCube(transform.position + damageBoxOffset, damageBoxSize);
        Gizmos.color = new Color(damageBoxColor.r, damageBoxColor.g, damageBoxColor.b, 0.25f);
        Gizmos.DrawCube(transform.position + damageBoxOffset, damageBoxSize);
    }

    public override void Update()
    {
        base.Update();
        MoveProjectile();
        AttackZone();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        transform.localPosition = iniPosition;
    }

    public virtual void MoveProjectile()
    {
        if (!moveProjectile) return;

        transform.Translate(moveDirection * projectileSpeed * Time.deltaTime, UnityEngine.Space.Self);
    }

    public virtual void AttackZone()
    {
        if (!canAttack) return;

        Collider[] colliders = Physics.OverlapBox(transform.position + damageBoxOffset, damageBoxSize / 2, Quaternion.identity, targetLayer, QueryTriggerInteraction.Collide);

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

    [Button]
    public virtual void StartAttack()
    {
        canAttack = true;
        moveProjectile = true;
    }
}
