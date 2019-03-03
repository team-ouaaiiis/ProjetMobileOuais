using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProceduralMapTextureReader : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private RenderTexture rt;
    [SerializeField] private Camera cam;
    [SerializeField] private ProceduralMapManager map;

    [Header("Ground")]
    [SerializeField] private Color groundColor;
    [SerializeField] private float range = 0.05f;
    private Texture2D tex2D;

    [Header("Cities")]
    [SerializeField] [Range(0,1)] private float cityRate = 0.1f;
    [SerializeField] private City[] cities;
    [SerializeField] private List<City> generatedCities = new List<City>();

    [Header("City Position")]
    [SerializeField] private float minPosOffset = 2f;
    [SerializeField] private float maxPosOffset = 5f;
    [SerializeField] private float edgeOffset = 20f;
    [SerializeField] private float cityNameOffset = 4f;
    
    [Header("City Size")]
    [SerializeField] private float minSize = 1f;
    [SerializeField] private float maxSize = 2f;
    [SerializeField] private float nameSize = 2f;

    [Header("City Names")]
    [SerializeField] private int maxSyllables = 3;
    [SerializeField] private string[] prefixes;
    [SerializeField] private string[] syllables;
    [SerializeField] private string[] suffixes;

    [Button("Disable Cities")]
    private void DisableCities()
    {
        for (int i = 0; i < generatedCities.Count; i++)
        {
            generatedCities[i].gameObject.SetActive(false);
        }

        generatedCities.Clear();
    }

    [Button("Generate Cities")]
    private void Check()
    {
        DisableCities();

        tex2D = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
        Rect rectReadPicture = new Rect(0, 0, rt.width, rt.height);
        RenderTexture.active = rt;

        // Read pixels
        tex2D.ReadPixels(rectReadPicture, 0, 0);
        tex2D.Apply();

        for (int x = 0; x < tex2D.width; x++)
        {
            for (int y = 0; y < tex2D.height; y++)
            {
                Debug.Log("Analysing");
                float r = tex2D.GetPixel(x, y).r;
                float g = tex2D.GetPixel(x, y).g;
                float b = tex2D.GetPixel(x, y).b;
                if(IsInRange(groundColor.r, range, r) && IsInRange(groundColor.g, range, g) && IsInRange(groundColor.b, range, b))
                {
                    float xPos = CustomMethod.Interpolate(-map.MapSize / 2 + edgeOffset, map.MapSize / 2 - edgeOffset, 0, rt.width, x);
                    float yPos = CustomMethod.Interpolate(-map.MapSize / 2 + edgeOffset, map.MapSize / 2 - edgeOffset, 0, rt.height, y);
                    GenerateCities(xPos, yPos);
                }
            }
        }

        RenderTexture.active = null; // added to avoid errors 
    }
    
    private void GenerateCities(float x, float y)
    {      
        if(Random.Range(0f,1f) < cityRate)
        {
            Debug.Log("New City");
            City newCity = PoolCity();
            newCity.gameObject.SetActive(true);
            float offsetX = x + Random.Range(minPosOffset, maxPosOffset) * Mathf.Sign(Random.Range(-1, 1));
            float offsetY = y + Random.Range(minPosOffset, maxPosOffset) * Mathf.Sign(Random.Range(-1, 1));
            Vector2 pos = new Vector3(offsetX, offsetY);
            float scale = Random.Range(minSize, maxSize);
            newCity.InitializeCity(CityName(), pos, scale, nameSize);
            generatedCities.Add(newCity);
        }
    }

    private string CityName()
    {
        string syl = "";
        for (int i = 0; i < Random.Range(0,maxSyllables); i++)
        {
            syl += syllables[Random.Range(0, syllables.Length - 1)];
        }

        return prefixes[Random.Range(0, prefixes.Length - 1)] + syl + suffixes[Random.Range(0, suffixes.Length - 1)];
    }

    private City PoolCity()
    {
        for (int i = 0; i < cities.Length; i++)
        {
            if(!cities[i].gameObject.activeInHierarchy)
            {
                return cities[i];
            }
        }

        Debug.Log("No City left in the pool");
        return null;
    }

    private bool IsInRange(float baseFloat, float range, float checkedFloat)
    {
        if (checkedFloat <= baseFloat + range && checkedFloat > baseFloat - range)
        {
            return true;
        }

        else
            return false;
    }
}
