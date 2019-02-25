using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BiomeManager : Manager
{
    [SerializeField] private List<BiomeAsset> biomes = new List<BiomeAsset>();

    [Dropdown("biomeNames")]
    [SerializeField] private string currentBiome;

    private BiomeAsset currentBiomeAsset;
    private List<string> biomeNames = new List<string>();

    public override void Awake()
    {
        GameManager.instance.BiomeManager = this;
    }

    [Button]
    private void Apply()
    {        
        biomeNames.Clear();
        for (int i = 0; i < biomes.Count; i++)
        {
            biomeNames.Add(biomes[i].name);
        }
    }

    public void CurrentBiomeUpdate()
    {
        CurrentBiomeAsset = biomes[biomeNames.IndexOf(currentBiome)];
    }

    #region Properties

    public BiomeAsset CurrentBiomeAsset { get => currentBiomeAsset; set => currentBiomeAsset = value; }

    #endregion
}
