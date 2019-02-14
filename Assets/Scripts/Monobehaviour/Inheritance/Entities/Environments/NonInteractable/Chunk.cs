using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : NonInteractable
{
    #region Fields

    [Header("Chunk")]
    [SerializeField] private List<Entity> chunkMembers = new List<Entity>();
    private ChunkManager chunkManager;

    #endregion

    #region Monobehaviour Callbacks

    public override void Start()
    {
        base.Start();
        chunkManager = GameManager.instance.ChunkManager;
    }

    public override void Update()
    {
        base.Update();
        CheckDistance();
        Scrolling();
    }

    #endregion

    #region Private Methods

    private void CheckDistance()
    {
        if (transform.position.z <= -chunkManager.ChunkSize)
        {
            DeactivateChunk();
        }
    }
    
    private void Scrolling()
    {
        float zScroll = Time.deltaTime * chunkManager.ScrollSpeed;
        transform.localPosition -= new Vector3(0, 0, zScroll);
    }

    private void DeactivateChunk()
    {
        Debug.Log("Deactivate");
        GameManager.instance.ChunkManager.SpawnNewChunk();
        gameObject.SetActive(false);
    }

    #endregion
}
