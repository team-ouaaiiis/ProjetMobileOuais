﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChunkDifficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class ChunkElement
{
    [SerializeField] private Entity entity;
    [SerializeField] private GameObject go;
    [SerializeField] private int xPos;
    [SerializeField] private int zPos;

    public Entity Entity { get => entity; set => entity = value; }
    public int XPos { get => xPos; set => xPos = value; }
    public int ZPos { get => zPos; set => zPos = value; }
    public GameObject Go { get => go; set => go = value; }
}

[CreateAssetMenu]
public class ChunkPattern : ScriptableObject
{
    #region Fields

    [SerializeField] private ChunkDifficulty chunkDifficulty;
    [SerializeField] private List<ChunkElement> chunkElements = new List<ChunkElement>();
    [SerializeField] private bool isSaved = false;
    [SerializeField] private List<ChunkElement> chunkElementsSave = new List<ChunkElement>();

    #endregion

    public void Initialize()
    {
        for (int i = 0; i < chunkElements.Count; i++)
        {
            if (chunkElements[i].Go != null)
            {
                chunkElements[i].Entity = chunkElements[i].Go.GetComponent<Entity>();
            }
        }
    }

    public void ChangeSize(int size)
    {
        chunkElements.Clear();
        for (int i = 0; i < size; i++)
        {
            chunkElements.Add(chunkElementsSave[i]);
        }
    }

    public void Save()
    {
        chunkElementsSave = chunkElements;
        isSaved = true;
        Debug.Log("Saved Pattern");
    }

    public void CheckIfSave()
    {
        if(chunkElementsSave != chunkElements && isSaved)
        {
            isSaved = false;
        }
    }

    #region Properties

    public ChunkDifficulty ChunkDifficulty { get => chunkDifficulty; set => chunkDifficulty = value; }
    public List<ChunkElement> ChunkElements { get => chunkElements; set => chunkElements = value; }
    public bool IsSaved { get => isSaved; set => isSaved = value; }
    public List<ChunkElement> ChunkElementsSave { get => chunkElementsSave; set => chunkElementsSave = value; }

    #endregion
}
