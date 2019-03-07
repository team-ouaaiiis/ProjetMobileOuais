using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTravelledDistance : Entity
{

    public float travelledDistance = 0;
    ChunkManager chunkManager;

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
}
