using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WooshPlayer : MonoBehaviour
{
    public WooshManager wooshManager;

    private void OnEnable()
    {
        wooshManager = GetComponentInChildren<WooshManager>();
    }

    public void PlayWoosh()
    {
        wooshManager.PlayWoosh();
    }
}
