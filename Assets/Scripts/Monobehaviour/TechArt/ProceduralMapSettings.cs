using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ExecuteAlways]
public class ProceduralMapSettings : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] private Material[] mats;

    [Header("Settings")]
    [SerializeField] private float seed = 5f;
    [SerializeField] private float normalStrength = 50f;
    [Range(0,0.015f)] [SerializeField] private float voronoiDensity = 5f;
    [SerializeField] private Vector2 earthWaterRatio;
    [SerializeField] private Vector2 smoothstepWater;

    private void Update()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            mats[i].SetVector("_Voronoi", new Vector4(seed, voronoiDensity, 0, 0));
            mats[i].SetVector("_Levels", new Vector4(earthWaterRatio.x, earthWaterRatio.y, smoothstepWater.x, smoothstepWater.y));
            mats[i].SetFloat("_NormalStrength", normalStrength);
        }
    }
}
