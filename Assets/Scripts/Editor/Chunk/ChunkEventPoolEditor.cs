using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChunkEventsPool))]
[ExecuteInEditMode]
public class ChunkEventPoolEditor : Editor
{
    #region Fields

    [SerializeField] private SerializedObject GetTarget;
    [SerializeField] private ChunkEventsPool chunkEventPool;
    [SerializeField] private SerializedProperty eventPrefabs;
    [SerializeField] private SerializedProperty prefabNumber;

    #endregion

    private void OnEnable()
    {
        chunkEventPool = (ChunkEventsPool)target;
        GetTarget = new SerializedObject(chunkEventPool);
        eventPrefabs = GetTarget.FindProperty("eventPrefabs");
        prefabNumber = GetTarget.FindProperty("prefabNumber");
    }

    public override void OnInspectorGUI()
    {
        InstantiatePrefabsButton();
    }

    private void InstantiatePrefabsButton()
    {
        EditorGUI.BeginChangeCheck();
        GetTarget.Update();
        Space(2);

        EditorGUILayout.PropertyField(prefabNumber);

        if(chunkEventPool.PrefabNumber < 0)
        {
            chunkEventPool.PrefabNumber = 0;
        }

        Space(2);

        EditorGUILayout.PropertyField(eventPrefabs, true);

        Space(2);

        if(!chunkEventPool.HasInstantiatedPrefabs)
        {
            if (GUILayout.Button("Instantiate Prefabs"))
            {
                InstantiatePrefabs();
            }
        }

        else
        {
            if (GUILayout.Button("Destroy Prefabs"))
            {
                DestroyPrefabs();
            }
        }        

        GetTarget.ApplyModifiedProperties();
    }

    private void InstantiatePrefabs()
    {
        Undo.RecordObject(target, "Instantiate Prefabs");

        for (int i = 0; i < chunkEventPool.EventPrefabs.Length; i++)
        {
            chunkEventPool.EventPools.Add(new EventPool());
            GameObject holder = new GameObject();
            holder.name = chunkEventPool.EventPrefabs[i].gameObject.name + "_Event_Holder";
            holder.transform.parent = chunkEventPool.EventHolder;
            chunkEventPool.Holders.Add(holder);

            for (int x = 0; x < chunkEventPool.PrefabNumber; x++)
            {
                GameObject newPrefab = PrefabUtility.InstantiatePrefab(chunkEventPool.EventPrefabs[i]) as GameObject;
                newPrefab.SetActive(false);

                Entity entity = newPrefab.GetComponentInChildren<Entity>();
                chunkEventPool.EventPools[i].EventEntities.Add(entity);
                newPrefab.transform.parent = holder.transform;                
                entity.Holder = holder.transform;
            }
        }

        chunkEventPool.HasInstantiatedPrefabs = true;
    }

    private void DestroyPrefabs()
    {
        Undo.RecordObject(target, "Destroy Prefabs");
        
        for (int i = 0; i < chunkEventPool.EventPools.Count; i++)
        {
            for (int x = 0; x < chunkEventPool.EventPools[i].EventEntities.Count; x++)
            {
                DestroyImmediate(chunkEventPool.EventPools[i].EventEntities[x].gameObject);
            }
        }

        for (int i = 0; i < chunkEventPool.Holders.Count; i++)
        {
            DestroyImmediate(chunkEventPool.Holders[i]);
        }

        chunkEventPool.Holders.Clear();
        chunkEventPool.EventPools.Clear();
        chunkEventPool.HasInstantiatedPrefabs = false;
    }

    #region Public Methods

    public virtual void Space(int spaceCount)
    {
        for (int i = 0; i < spaceCount; i++)
        {
            EditorGUILayout.Space();
        }
    }

    public virtual void Space()
    {
        EditorGUILayout.Space();
    }

    #endregion
}