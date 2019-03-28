using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System.IO;
using SimpleJSON;

public class SavedData : MonoBehaviour
{
    [ReadOnly] public string path;
    public string pathCopy;
    public string fileName = "UnknownData";

    public virtual void Awake()
    {
        SetPath();
    }

    public virtual void Start()
    {
        DataManager.instance.AddSavedData(this);
    }

    [Button]
    public virtual void SaveData()
    {
        SetPath();
    }

    [Button]
    public virtual void LoadData()
    {

    }

    [Button]
    public virtual void DeleteData()
    {

#if UNITY_EDITOR
        SetPath();
        File.Delete(path);
#endif

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    void SetPath()
    {
        if(!Directory.Exists(Application.persistentDataPath + "/Data"))
        {
            path = Application.persistentDataPath + "/Data";
            Directory.CreateDirectory(path);
        }

        pathCopy = path = Application.persistentDataPath + "/Data";
        string file = "/" + fileName + ".json";
        path = path + file;
        
    }
}
