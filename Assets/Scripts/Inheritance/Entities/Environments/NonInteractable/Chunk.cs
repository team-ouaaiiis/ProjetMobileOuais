using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : NonInteractable
{
    [Header("Chunk")]
    [SerializeField] private List<Entity> chunkMembers = new List<Entity>();
    [SerializeField] private Transform chunkPool;

    public override void Update()
    {
        base.Update();
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (transform.position.z < 20f)
        {
            DeactivateChunk();
        }
    }

    private void DeactivateChunk()
    {
        Debug.Log("Deactivate");
        transform.parent = chunkPool;
        gameObject.SetActive(false);
    }
}
