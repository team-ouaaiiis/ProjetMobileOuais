using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : NonInteractable
{
    #region Fields

    [Header("Chunk")]
    [SerializeField] private List<Entity> chunkMembers = new List<Entity>();
    [SerializeField] private Transform chunkPool;

    #endregion

    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
        CheckDistance();
    }

    #endregion

    #region Private Methods

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

    #endregion
}
