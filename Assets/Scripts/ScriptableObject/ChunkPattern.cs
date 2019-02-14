using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkDifficulty
{
    Easy,
    Medium,
    Hard
}

[CreateAssetMenu]
public class ChunkPattern : ScriptableObject
{
    #region Fields

    [SerializeField] private ChunkDifficulty chunkDifficulty;

    #endregion

    #region Properties

    public ChunkDifficulty ChunkDifficulty { get => chunkDifficulty; set => chunkDifficulty = value; }

    #endregion
}
