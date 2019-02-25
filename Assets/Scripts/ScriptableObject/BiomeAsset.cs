using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BiomeAsset : ScriptableObject
{
    #region Fields

    [SerializeField] private ChunkPattern[] biomeSpecificPatterns;
    [SerializeField] [Range(0,1)][Tooltip("La probabilité de spawn un pattern spécifique au Biome, 0 = impossible, 1 = tout le temps")]
    private float specificPatternRate = 0.5f;

    #endregion


    #region Properties

    public ChunkPattern[] BiomeSpecificPatterns { get => biomeSpecificPatterns; set => biomeSpecificPatterns = value; }
    public float SpecificPatternRate { get => specificPatternRate; set => specificPatternRate = value; }

    #endregion
}
