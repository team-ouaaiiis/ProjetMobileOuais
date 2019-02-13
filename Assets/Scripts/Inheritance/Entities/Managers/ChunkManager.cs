using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : Manager
{
    #region Fields 

    [Header("Components")]
    [SerializeField] private List<Chunk> chunks = new List<Chunk>();
    [SerializeField] private Transform chunkScroller;

    [Header("Parameters")]
    [SerializeField] private float scrollSpeed = 5f;
    [SerializeField] private float chunkSize = 20f;

    #endregion

    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
        Scrolling();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.instance.ChunkManager = this;
    }

    #endregion

    private void Scrolling()
    {
        float zScroll = Time.deltaTime * scrollSpeed;
        chunkScroller.position -= new Vector3(0, 0, zScroll);
    }

    public void SpawnNewChunk()
    {
        Chunk newChunk = NewChunk();
        newChunk.gameObject.SetActive(true);
        newChunk.transform.localPosition = new Vector3(0,0, chunkSize);
        newChunk.transform.parent = chunkScroller;
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