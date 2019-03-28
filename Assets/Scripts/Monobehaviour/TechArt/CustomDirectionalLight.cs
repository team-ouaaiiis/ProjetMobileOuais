using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ExecuteInEditMode]
public class CustomDirectionalLight : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private List<Material> mats = new List<Material>();

    [Header("Light Settings")]
    [SerializeField] private Color lightCol = Color.white;
    [SerializeField] private Color shadowColor = Color.gray;
    [SerializeField] private float lightIntensity = 0f;
    [SerializeField] [Range(0,1f)] private float shadowSharpness = 0.2f;
    [SerializeField] private List<MeshRenderer> renderers = new List<MeshRenderer>();

    private void Update()
    {
        MaterialUpdate();
    }

    [Button]
    private void FindRenderers()
    {
        renderers.Clear();
        MeshRenderer[] rends = FindObjectsOfType<MeshRenderer>();
        for (int i = 0; i < rends.Length; i++)
        {
            renderers.Add(rends[i]);
        }
    }

    private void MaterialUpdate()
    {
        //for (int i = 0; i < mats.Count; i++)
        //{
        //    mats[i].SetVector("_LightDir", transform.forward);
        //    mats[i].SetColor("_AddColor", lightCol);
        //    mats[i].SetColor("_ShadowColor", shadowColor);
        //    mats[i].SetFloat("_AddedStrength", lightIntensity);
        //    //mats[i].SetFloat("_ShadowStrength", shadowStrength);
        //    mats[i].SetFloat("_ShadowSharp", shadowSharpness / 2);
        //}

        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].sharedMaterial.SetVector("_LightDir", transform.forward);
            renderers[i].sharedMaterial.SetColor("_AddColor", lightCol);
            renderers[i].sharedMaterial.SetColor("_ShadowColor", shadowColor);
            renderers[i].sharedMaterial.SetFloat("_AddedStrength", lightIntensity);
            renderers[i].sharedMaterial.SetFloat("_ShadowSharp", shadowSharpness / 2);
        }

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
    }
}
