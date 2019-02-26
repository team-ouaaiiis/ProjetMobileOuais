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
    [SerializeField] private List<ChunkElement> elemSave = new List<ChunkElement>(40);

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
        Debug.Log("Changed Size");
        if(chunkElements.Count > 0)
        {
            for (int i = 0; i < chunkElements.Count; i++)
            {
                elemSave[i].Go = chunkElements[i].Go;
            }
        }

        chunkElements.Clear();

        for (int i = 0; i < size; i++)
        {
            chunkElements.Add(new ChunkElement());
        }

        for (int i = 0; i < chunkElements.Count; i++)
        {
            chunkElements[i].Go = elemSave[i].Go;
        }
    }    

    public void CheckElemSaveSize()
    {
        if(elemSave.Count != 40)
        {
            Debug.Log("Resized Save");
            elemSave.Clear();
            for (int i = 0; i < 40; i++)
            {
                elemSave.Add(new ChunkElement());
            }
        }
    }

    public void Clear()
    {
        for (int i = 0; i < elemSave.Count; i++)
        {
            chunkElements[i] = null;
            elemSave[i] = null;
        }
    }

    #region Properties

    public ChunkDifficulty ChunkDifficulty { get => chunkDifficulty; set => chunkDifficulty = value; }
    public List<ChunkElement> ChunkElements { get => chunkElements; set => chunkElements = value; }

    #endregion
}
