using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChunkManagerReferencer : MonoBehaviour
{
    [SerializeField] private ChunkManager chunkManager;
    public static ChunkManagerReferencer instance;

    private void Update()
    {
        instance = this;
    }

    public ChunkManager ChunkManager { get => chunkManager; set => chunkManager = value; }
}
