using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : Manager
{
    #region Fields 
    [Header("Patterns")]
    [SerializeField] private ChunkPattern[] generalPatterns;

    [Header("Dimensions")]
    [SerializeField] [Range(0,8)] private int rows = 5;
    [SerializeField] [Range(0,5)] private int columns = 3;
    [SerializeField] private float chunkLength = 20f;
    [SerializeField] private float spawnZoneLength = 18f;
    [SerializeField] private float chunkWidth = 5f;

    [Header("Components")]
    [SerializeField] private List<Chunk> chunkPool = new List<Chunk>();
    [SerializeField] private ChunkEventsPool chunkEventsPool;

    [Header("Parameters")]    
    [SerializeField] private float scrollSpeed = 5f;

    #endregion

    #region Monobehaviour Callbacks

    public override void Awake()
    {
        base.Awake();
        GameManager.instance.ChunkManager = this;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void Start()
    {
        base.Start();
    }

    #endregion

    public void InitializePatterns()
    {
        for (int i = 0; i < generalPatterns.Length; i++)
        {
            generalPatterns[i].Initialize();
        }        
    }

    public void SpawnNewChunk()
    {
        Chunk newChunk = NewChunk();
        newChunk.gameObject.SetActive(true);
        newChunk.transform.localPosition = new Vector3(0, 0, GetFurthestChunkZ() + chunkLength);
        
        if(GameManager.instance.BiomeManager.CurrentBiomeAsset.SpecificPatternRate < Random.Range(0 , 1f)) //Chaque Biome possède des Chunks Patterns spécifiques, ainsi qu'une probabilité d'utiliser l'un de ces chunks patterns (entre 0 et 1)
        {
            newChunk.InitializeChunk(generalPatterns[Random.Range(0, generalPatterns.Length)]); //Si la variable est inférieure à un float random entre 0 et 1f, on choisit un pattern "Général".
        }

        else
        {
            Debug.Log("Spawning Biome Chunk");
            newChunk.InitializeChunk(GameManager.instance.BiomeManager.CurrentBiomeAsset.BiomeSpecificPatterns[Random.Range(0, GameManager.instance.BiomeManager.CurrentBiomeAsset.BiomeSpecificPatterns.Length)]);
        } //Sinon, on prend un des patterns spécifique au Biome.

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
    public float SpawnZoneLength { get => spawnZoneLength; set => spawnZoneLength = value; }

    #endregion
}