using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;

public class PlayerJourneyData : SavedData
{
    [Header("Journey data")]
    public int numberOfJourney;
    public List<PlayerJourney> playerJourneys;
    JSONObject journeyJSON = new JSONObject();

    public static PlayerJourneyData instance;

    public override void Awake()
    {
        base.Awake();
        instance = this;
    }

    public override void SaveData()
    {
        base.SaveData();

        //Journey data

        journeyJSON.Add("numberOfJourney", numberOfJourney);

        JSONArray journeys = new JSONArray();

        for (int i = 0; i < playerJourneys.Count; i++)
        {
            journeys.Add(playerJourneys[i].journeyDistance);
        }

        journeyJSON.Add("playerJourneys", journeys);

        File.WriteAllText(path, journeyJSON.ToString());
        Debug.Log("SAVED DATA");
    }

    public override void LoadData()
    {
        base.LoadData();

        string jsonString = File.ReadAllText(path);
        journeyJSON = (JSONObject)JSON.Parse(jsonString);

        numberOfJourney = journeyJSON["numberOfJourney"];

        playerJourneys = new List<PlayerJourney>();

        for (int i = 0; i < numberOfJourney; i++)
        {
            PlayerJourney _journey = new PlayerJourney(); 
            _journey.journeyDistance = journeyJSON["playerJourneys"].AsArray[i];
            playerJourneys.Add(_journey);
        }

        Debug.Log("LOADED DATA");
    }

    public void UpdateData(PlayerJourney _journeyToAdd)
    {
        if (playerJourneys.Contains(_journeyToAdd))
        {
            Debug.Log("Journey already added");
            return;
        }

        playerJourneys.Add(_journeyToAdd);
        numberOfJourney = playerJourneys.Count;
    }
}

[System.Serializable]
public class PlayerJourney
{
    public float journeyDistance;
}
