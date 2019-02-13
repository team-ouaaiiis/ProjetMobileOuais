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
        if (transform.position.z < -GameManager.instance.ChunkManager.ChunkSize)
        {
            DeactivateChunk();
        }
    }

    private void DeactivateChunk()
    {
        Debug.Log("Deactivate");
        transform.parent = chunkPool;
        GameManager.instance.ChunkManager.SpawnNewChunk();
        gameObject.SetActive(false);
    }
}
