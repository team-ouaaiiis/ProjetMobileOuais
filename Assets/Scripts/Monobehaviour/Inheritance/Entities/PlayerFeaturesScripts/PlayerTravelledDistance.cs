using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class PlayerTravelledDistance : Entity
{

    public PlayerJourney journey;
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
        journey.journeyDistance += chunkManager.ScrollSpeed/2 * Time.deltaTime;
    }

    [Button]
    public void SaveDistanceData()
    {
        //SAVE DATA
        PlayerJourneyData journeyData = PlayerJourneyData.instance;
        journeyData.UpdateData(journey);
        journeyData.SaveData();
    }

    void PLACEHOLDER_DistanceText()
    {
        if (textMesh == null) return;

        int dis = Mathf.RoundToInt(journey.journeyDistance);
        textMesh.text = dis.ToString();
    }
}
