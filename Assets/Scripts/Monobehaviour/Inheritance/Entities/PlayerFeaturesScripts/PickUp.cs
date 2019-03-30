using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PickUp : Entity
{
    [Header("Pick up values")]
    public LayerMask pickableObjectLayer;
    public bool canPickObject = true;

    [SerializeField, BoxGroup("Pick Up Box")] Vector3 boxSize = Vector3.one;
    [SerializeField, BoxGroup("Pick Up Box")] Vector3 boxOffset;
    [SerializeField, BoxGroup("Pick Up Box")] Color boxDebugColor = Color.magenta;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = CustomMethod.GetColorWithAlpha(boxDebugColor, 1);
        Gizmos.DrawWireCube(transform.position + boxOffset, boxSize);
        Gizmos.color = CustomMethod.GetColorWithAlpha(boxDebugColor, 0.4f);
        Gizmos.DrawCube(transform.position + boxOffset, boxSize);
    }

    public override void Update()
    {
        base.Update();
        PickObject();
    }

    void PickObject()
    {
        if (!canPickObject) return;

        Collider[] colliders = Physics.OverlapBox(transform.position + boxOffset, boxSize / 2, Quaternion.identity, pickableObjectLayer, QueryTriggerInteraction.Collide);

        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                IPickableObject pickableObject = colliders[i].GetComponent<IPickableObject>();
                if (pickableObject != null)
                {
                    pickableObject.GetObject();
                }
            }
        }
    }
}
