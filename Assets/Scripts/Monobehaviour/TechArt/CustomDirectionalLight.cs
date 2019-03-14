using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomDirectionalLight : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private List<Material> mats = new List<Material>();
    [SerializeField] private Camera camShadows;
    [SerializeField] private GameObject shadowPlane;

    [Header("Light Settings")]
    [SerializeField] private Color lightCol = Color.white;
    [SerializeField] private Color shadowColor = Color.gray;
    [SerializeField] private float lightIntensity = 0f;
    //[SerializeField] private float shadowStrength = 0.2f;
    [SerializeField] [Range(0,1f)] private float shadowSharpness = 0.2f;

    private void Update()
    {
        MaterialUpdate();
        if(shadowPlane != null)
            ShadowManager();
    }

    private void MaterialUpdate()
    {
        for (int i = 0; i < mats.Count; i++)
        {
            mats[i].SetVector("_LightDir", transform.forward);
            mats[i].SetColor("_AddColor", lightCol);
            mats[i].SetColor("_ShadowColor", shadowColor);
            mats[i].SetFloat("_AddedStrength", lightIntensity);
            //mats[i].SetFloat("_ShadowStrength", shadowStrength);
            mats[i].SetFloat("_ShadowSharp", shadowSharpness / 2);
        }

        Debug.DrawRay(transform.position, transform.forward * 10, Color.yellow);

    }

    private void ShadowManager()
    {
        //shadowPlane.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
