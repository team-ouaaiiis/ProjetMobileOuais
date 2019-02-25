using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerHitbox : Entity, IDamageListener
{
    Player player;

    [BoxGroup("Debug")]
    public bool showDebug = true;

    [SerializeField, BoxGroup("Debug"), ShowIf("showDebug")] Color debugColor = Color.green;
    [SerializeField, BoxGroup("Debug"), ShowIf("showDebug"), Range(0.0f,1.0f)] float alpha = 0.4f;


    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        player = Player.playerInstance;
    }

    public override void TakeDamage(float dmg)
    {
        //base.TakeDamage(dmg);
        player.TakeDamage(dmg);
    }

    public void OnDrawGizmos()
    {
        if (!showDebug) return;

        BoxCollider box = GetComponent<BoxCollider>();

        if(box != null)
        {
            Gizmos.color = new Color(debugColor.r, debugColor.g, debugColor.b, alpha);
            Gizmos.DrawCube(transform.position + box.center, box.size);
        }
    }
}
