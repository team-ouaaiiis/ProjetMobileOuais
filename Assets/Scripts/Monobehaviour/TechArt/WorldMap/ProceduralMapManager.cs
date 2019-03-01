using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProceduralMapManager : Manager
{
    [Header("Map Settings")]
    [SerializeField] private float mapSize = 1f;
    [SerializeField] private Camera[] cams;
    [SerializeField] private GameObject[] maps;

    [Header("Map Generation")]
    [SerializeField] private GameObject[] toDeactivate;

    public override void Start()
    {
        base.Start();
        StartCoroutine(Deactivating());
    }

    private IEnumerator Deactivating()
    {
        yield return new WaitForSecondsRealtime(.5f);
        DeactivateRealtimeMap();
    }

    [Button("Deactivate")]
    private void DeactivateRealtimeMap()
    {
        for (int i = 0; i < toDeactivate.Length; i++)
        {
            toDeactivate[i].SetActive(false);
        }
    }

    [Button("Activate")]
    private void ActivateRealtimeMap()
    {
        for (int i = 0; i < toDeactivate.Length; i++)
        {
            toDeactivate[i].SetActive(true);
        }
    }

    private void RefreshMap()
    {
    }

    [Button("Resize Map")]
    private void ResizeMap()
    {
        for (int i = 0; i < maps.Length; i++)
        {
            maps[i].transform.localScale = new Vector3(MapSize, MapSize, MapSize);
        }

        for (int i = 0; i < cams.Length; i++)
        {
            cams[i].orthographicSize = MapSize / 2;
        }
    }

    public float MapSize { get => mapSize; set => mapSize = value; }

}
