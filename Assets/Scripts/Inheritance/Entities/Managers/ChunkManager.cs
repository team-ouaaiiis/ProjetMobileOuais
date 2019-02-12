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

    #endregion

    #region Monobehaviour Callbacks

    public override void Update()
    {
        base.Update();
        Scrolling();
    }

    #endregion

    private void Scrolling()
    {
        float zScroll = Time.deltaTime * scrollSpeed;
        chunkScroller.position += new Vector3(0, 0, zScroll);
    }

    #region Properties

    public float ScrollSpeed { get => scrollSpeed; set => scrollSpeed = value; }

    #endregion
}