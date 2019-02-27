using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProceduralMapTextureReader : MonoBehaviour
{
    [SerializeField] private RenderTexture rt;
    [SerializeField] private Camera cam;
    [SerializeField] private Color cityColor;
    [SerializeField] private float range = 0.05f;
    private Texture2D tex2D;

    [Button]
    private void Check()
    {
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
                if(IsInRange(cityColor.r, range, r) && IsInRange(cityColor.g, range, g) && IsInRange(cityColor.b, range, b))
                {
                    Debug.Log("Detected City");
                }
            }
        }

        RenderTexture.active = null; // added to avoid errors 
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
