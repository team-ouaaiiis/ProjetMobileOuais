using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class Enemy : Character, IDamageListener
{
    [Header("Enemy values")]
    public float attackRange = 1;
    [SerializeField] Vector3 hitboxSize = Vector3.one;
    [SerializeField] public Vector3 hitboxOffset;
    [SerializeField] LayerMask playerTargetLayer;

    [BoxGroup("Debug"), SerializeField] Color hitboxColor = Color.cyan;
    

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

    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg);
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
