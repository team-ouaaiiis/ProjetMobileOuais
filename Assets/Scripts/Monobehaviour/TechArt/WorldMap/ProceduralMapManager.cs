using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class ProceduralMapManager : Manager
{
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

    [Button]
    private void DeactivateRealtimeMap()
    {
        for (int i = 0; i < toDeactivate.Length; i++)
        {
            toDeactivate[i].SetActive(false);
        }
    }

    [Button]
    private void ActivateRealtimeMap()
    {
        for (int i = 0; i < toDeactivate.Length; i++)
        {
            toDeactivate[i].SetActive(true);
        }
    }
}
