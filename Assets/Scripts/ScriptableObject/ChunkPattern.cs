using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkDifficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public struct ChunkElement
{
    [SerializeField] private GameObject prefab;

    public GameObject Prefab { get => prefab; set => prefab = value; }
}

[CreateAssetMenu]
public class ChunkPattern : ScriptableObject
{
    #region Fields

    [SerializeField] private ChunkDifficulty chunkDifficulty;
    [SerializeField] private List<ChunkElement> chunkElements = new List<ChunkElement>();

    #endregion    

    public void ChangeSize(int size)
    {
        chunkElements.Clear();
        for (int i = 0; i < size; i++)
        {
            chunkElements.Add(new ChunkElement());
        }
    }

    #region Properties

    public ChunkDifficulty ChunkDifficulty { get => chunkDifficulty; set => chunkDifficulty = value; }
    public List<ChunkElement> ChunkElements { get => chunkElements; set => chunkElements = value; }

    #endregion
}
