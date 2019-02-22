using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : Manager
{
    #region Fields 

    [Header("Dimensions")]
    [SerializeField] [Range(0,8)] private int rows = 5;
    [SerializeField] [Range(0,5)] private int columns = 3;

    [Header("Components")]
    [SerializeField] private List<Chunk> chunkPool = new List<Chunk>();
    [SerializeField] private ChunkPattern[] chunkPatterns;
    [SerializeField] private ChunkEventsPool chunkEventsPool;

    [Header("Parameters")]    
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private float chunkWidth = 5f;

    #endregion

    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        AssignToGameManager();
    }

    #endregion

    [ContextMenu("Assign")]
    private void AssignToGameManager()
    {
        GameManager.instance.ChunkManager = this;
    }

    public void SpawnNewChunk()
    {
        Chunk newChunk = NewChunk();
        newChunk.gameObject.SetActive(true);
        newChunk.transform.localPosition = new Vector3(0, 0, GetFurthestChunkZ() + chunkLength);
        newChunk.InitializeChunk(chunkPatterns[Random.Range(0,chunkPatterns.Length)]);
    }

    private Chunk NewChunk()
    {
        for (int i = 0; i < chunkPool.Count; i++)
        {
            if(!chunkPool[i].gameObject.activeInHierarchy)
            {
                return chunkPool[i];
            }
        }

        return null;    
    }

    private float GetFurthestChunkZ()
    {
        Chunk furthest = chunkPool[0];
        for (int i = 0; i < chunkPool.Count; i++)
        {
            if (chunkPool[i].transform.position.z >= furthest.transform.position.z)
            {
                furthest = chunkPool[i];
            }
        }

        return furthest.transform.position.z;
    }

    #region Properties

    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }
    public float ChunkLength
    {
        get
        {
            return chunkLength;
        }

        set
        {
            chunkLength = value;
        }
    }
    public int Rows { get => rows; set => rows = value; }
    public int Columns { get => columns; set => columns = value; }
    public float ChunkWidth
    {
        get
        {
            return chunkWidth;
        }

        set
        {
            chunkWidth = value;
        }
    }

    public ChunkEventsPool ChunkEventsPool { get => chunkEventsPool; set => chunkEventsPool = value; }

    #endregion
}