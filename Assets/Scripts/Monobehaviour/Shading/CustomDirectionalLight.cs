using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomDirectionalLight : MonoBehaviour
{
    [SerializeField] private List<Material> mats = new List<Material>();
    [SerializeField] private Color lightCol = Color.white;
    [SerializeField] private Color shadowColor = Color.gray;
    [SerializeField] private float lightIntensity = 0f;
    [SerializeField] private float shadowStrength = 0.2f;

    private void Update()
    {
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetVector("_LightDir", transform.forward);
            mats[i].SetColor("_AddColor", lightCol);
            mats[i].SetColor("_ShadowColor", shadowColor);
            mats[i].SetFloat("_AddedStrength", lightIntensity);
            mats[i].SetFloat("_ShadowStrength", shadowStrength);
        }

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);
    }
}
