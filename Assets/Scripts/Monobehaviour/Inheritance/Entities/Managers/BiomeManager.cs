using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BiomeManager : Manager
{
    [SerializeField] private List<BiomeAsset> biomes = new List<BiomeAsset>();
    
    [SerializeField] private int currentBiome;

    private BiomeAsset currentBiomeAsset;

    public override void Awake()
    {
        GameManager.instance.BiomeManager = this;
    }

    public override void Start()
    {
        base.Start();        
    }

    [Button("Initialize Patterns")]
    public void InitializePatterns()
    {
        Debug.Log("Initialized Patterns.");
        for (int x = 0; x < biomes.Count; x++)
        {
            for (int i = 0; i < biomes[x].BiomeSpecificPatterns.Length; i++)
            {
                biomes[x].BiomeSpecificPatterns[i].Initialize();
            }
        }

        ChunkManagerReferencer.instance.ChunkManager.InitializePatterns();
    }

    public override void Update()
    {
        CurrentBiomeUpdate();
    }

    public void CurrentBiomeUpdate()
    {
        CurrentBiomeAsset = biomes[currentBiome];
    }

    #region Properties

    public BiomeAsset CurrentBiomeAsset { get => currentBiomeAsset; set => currentBiomeAsset = value; }

    #endregion
}
