using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;


public class DataManager : MonoBehaviour
{
    public List<SavedData> savedDatas;

    public static DataManager instance;

    private void Awake()
    {
        instance = this;
        Debug.Log("AWAKE");
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public void InitialiazeTEST()
    {
        
        Debug.Log("OUAIIIS JE SUIS LA REGARDE");
    }

    public void AddSavedData(SavedData data)
    {
        savedDatas.Add(data);
    }

    public void SaveAll()
    {

    }

    public void LoadAll()
    {

    }

    [Button("Delete All files")]
    public static void DeleteAll()
    {
        string path = Application.persistentDataPath + "/Data";

        if (!Directory.Exists(path))
        {
            Debug.LogError("NO DATA FOUND");
            return;
        }

        string[] files =  Directory.GetFiles(path);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        Directory.Delete(path);
    }
}
