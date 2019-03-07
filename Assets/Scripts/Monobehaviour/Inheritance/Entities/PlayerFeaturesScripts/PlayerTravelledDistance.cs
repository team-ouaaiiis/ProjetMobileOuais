using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class PlayerTravelledDistance : Entity
{

    public float travelledDistance = 0;
    ChunkManager chunkManager;

    [BoxGroup("DEBUG PLACEHOLDER"), SerializeField] TextMeshProUGUI textMesh;

    public override void Awake()
    {
        base.Awake();
        //LOAD DATA
    }

    public override void Start()
    {
        base.Awake();
        chunkManager = GameManager.instance.ChunkManager; //Get chunk Manager reference
    }

    public override void Update()
    {
        base.Update();
        AddDistance();
        PLACEHOLDER_DistanceText();
    }

    public override void OnGameOver()
    {
        base.OnGameOver();
        SaveDistanceData();
    }

    public void AddDistance()
    {
        travelledDistance += chunkManager.ScrollSpeed/2 * Time.deltaTime;
    }

    public void SaveDistanceData()
    {
        //SAVE DATA
    }

    void PLACEHOLDER_DistanceText()
    {
        if (textMesh == null) return;

        int dis = Mathf.RoundToInt(travelledDistance);
        textMesh.text = dis.ToString();
    }
}
