using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ChunkManagerReferencer : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;
    public static ChunkManagerReferencer instance;

    public ChunkManager ChunkManager { get => chunkManager; set => chunkManager = value; }

    private void Update()
    {
        instance = this;
    }

}
