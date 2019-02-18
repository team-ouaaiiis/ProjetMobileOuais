using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chunk : NonInteractable
{
    #region Fields

    [Header("Chunk")]
    private ChunkManager chunkManager;
    private ChunkPattern chunkPattern;
    private List<Entity> chunkElements = new List<Entity>();

    #endregion

    #region Monobehaviour Callbacks

    public override void Start()
    {
        base.Start();
    }

    public override void OnEnable()
    {
        base.OnEnable();
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
        if (transform.position.z <= -chunkManager.ChunkLength)
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
        gameObject.SetActive(false);
        chunkManager.SpawnNewChunk();

        for (int i = 0; i < chunkElements.Count; i++)
        {
            //Replacer les entities dans les pools
        }
    }

    #endregion

    #region Public Methods

    public void InitializeChunk(ChunkPattern pattern)
    {
        chunkElements.Clear();
        chunkPattern = pattern;

        for (int i = 0; i < chunkPattern.ChunkElements.Count; i++)
        {
            if (chunkPattern.ChunkElements[i].Entity != null)
            {
                Entity newEntity = chunkManager.ChunkEventsPool.GetEntity(chunkPattern.ChunkElements[i].Entity);
                newEntity.gameObject.SetActive(true);
                newEntity.transform.parent = transform;
                float x = CustomMethod.Interpolate(chunkManager.ChunkWidth / 2, -chunkManager.ChunkWidth / 2, 0, chunkManager.Columns - 1, chunkPattern.ChunkElements[i].XPos);
                float z = CustomMethod.Interpolate(chunkManager.ChunkLength / 2, -chunkManager.ChunkLength / 2, 0, chunkManager.Rows - 1, chunkPattern.ChunkElements[i].ZPos);
                newEntity.gameObject.transform.localPosition = new Vector3(x, 0, z);
                chunkElements.Add(newEntity);
            }

        }
    }

    #endregion
}
