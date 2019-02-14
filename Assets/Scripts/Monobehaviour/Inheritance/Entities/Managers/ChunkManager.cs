using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : Manager
{
    #region Fields 

    [Header("Components")]
    [SerializeField] private List<Chunk> chunks = new List<Chunk>();

    [Header("Parameters")]
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float chunkSize = 20f;

    #endregion

    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.ChunkManager = this;
    }

    #endregion

    public void SpawnNewChunk()
    {
        Chunk newChunk = NewChunk();
        newChunk.gameObject.SetActive(true);
        newChunk.transform.localPosition = new Vector3(0, 0, GetFurthestChunkZ() + chunkSize);
    }

    private Chunk NewChunk()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            if(!chunks[i].gameObject.activeInHierarchy)
            {
                return chunks[i];
            }
        }

        return null;    
    }

    private float GetFurthestChunkZ()
    {
        Chunk furthest = chunks[0];
        for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i].transform.position.z >= furthest.transform.position.z)
            {
                furthest = chunks[i];
            }
        }

        return furthest.transform.position.z;
    }

    #region Properties

    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }

    public float ChunkSize
    {
        get
        {
            return chunkSize;
        }

        set
        {
            chunkSize = value;
        }
    }

    #endregion
}