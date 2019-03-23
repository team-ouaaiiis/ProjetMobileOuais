using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : NonInteractable
{
    #region Fields

    [Header("Chunk")]
    [HideInInspector] public ChunkManager chunkManager;
    private ChunkPattern chunkPattern;
    private List<Entity> chunkElements = new List<Entity>();

    #endregion

    #region Monobehaviour Callbacks

    public override void Start()
    {
        base.Start();

        if(ChunkManagerReferencer.instance == null)
        {
            Debug.Log("C LA MERD");
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        chunkManager = ChunkManagerReferencer.instance.ChunkManager;
    }

    public override void Update()
    {
        base.Update();
        CheckDistance();
        Scrolling();
    }

    #endregion

    #region Private Methods

    private void CheckDistance()
    {
        if(chunkManager != null)
        if (transform.position.z <= -chunkManager.ChunkLength)
        {
            DeactivateChunk();
        }
    }

    private void Scrolling()
    {
        float zScroll = Time.deltaTime * chunkManager.ScrollSpeed;
        transform.localPosition -= new Vector3(0, 0, zScroll);
    }

    private void DeactivateChunk()
    {
        for (int i = 0; i < ChunkElements.Count; i++)
        {
            //Replacer les entities dans les pools
            ChunkElements[i].transform.parent = ChunkElements[i].Holder;
            ChunkElements[i].gameObject.SetActive(false);   //argument out of range error sometimes? Killing an enemy at the frame where the chunk deactivates?
        }

        chunkManager.SpawnNewChunk();

        gameObject.SetActive(false);
    }

    #endregion

    #region Public Methods

    public void InitializeChunk(ChunkPattern pattern)
    {
        ChunkElements.Clear();
        chunkPattern = pattern;

        for (int i = 0; i < chunkPattern.ChunkElements.Count; i++)
        {
            if (chunkPattern.ChunkElements[i].Entity != null)
            {
                Entity newEntity = chunkManager.ChunkEventsPool.GetEntity(chunkPattern.ChunkElements[i].Entity);

                newEntity.gameObject.SetActive(true);
                newEntity.transform.parent = transform;
                newEntity.transform.localEulerAngles = new Vector3(0, 180, 0);
                float x = CustomMethod.Interpolate(-chunkManager.ChunkWidth / 2, chunkManager.ChunkWidth / 2, 0, chunkManager.Columns - 1, chunkPattern.ChunkElements[i].XPos);
                float z = CustomMethod.Interpolate(-chunkManager.SpawnZoneLength / 2, chunkManager.SpawnZoneLength / 2, 0, chunkManager.Rows - 1, chunkPattern.ChunkElements[i].ZPos);
                //Debug.Log("x = " + x, ", z = " + z);
                newEntity.gameObject.transform.localPosition = new Vector3(x, 0, z);
                ChunkElements.Add(newEntity);
                newEntity.ParentChunk = this;
            }
        }
    }

    #endregion

    #region Properties

    public List<Entity> ChunkElements { get => chunkElements; set => chunkElements = value; }

    #endregion
}
