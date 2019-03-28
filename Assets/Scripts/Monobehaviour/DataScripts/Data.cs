using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Data
{
    [MenuItem("Data/ Delete all data")]
    public static void DeleteAll()
    {
        string path = Application.persistentDataPath + "/Data";

        if (!Directory.Exists(path))
        {
            Debug.LogError("NO DATA FOUND");
            return;
        }

        string[] files = Directory.GetFiles(path);

        for (int i = 0; i < files.Length; i++)
        {
            File.Delete(files[i]);
        }

        Directory.Delete(path);
    }
}
