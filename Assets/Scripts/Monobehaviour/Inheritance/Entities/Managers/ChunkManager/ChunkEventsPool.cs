using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class EventPool
{
    [SerializeField] private List<Entity> entities = new List<Entity>();    

    public List<Entity> EventEntities { get => entities; set => entities = value; }
}

public class ChunkEventsPool : MonoBehaviour
{
    #region Fields

    [SerializeField] private int prefabNumber = 7;

    [Tooltip("La liste de tous les prefabs qu'on pourra instancier dans un Chunk (Enemies, Obstacles, Bonus...)")]
    [SerializeField] private GameObject[] eventPrefabs;
    [SerializeField] private List<EventPool> eventPools = new List<EventPool>();
    [SerializeField] private bool hasInstantiatedPrefabs = false;
    [SerializeField] private Transform eventHolder;
    [SerializeField] private List<GameObject> holders = new List<GameObject>();

    #endregion

    public Entity GetEntity(Entity entity)
    {
        for (int i = 0; i < eventPools.Count; i++)
        {
            if (eventPools[i].EventEntities[0].name == entity.name)
            {
                Debug.Log("Entity Match");
                for (int x = 0; x < eventPools[i].EventEntities.Count; x++)
                {
                    if(!eventPools[i].EventEntities[x].gameObject.activeInHierarchy)
                    {
                        return eventPools[i].EventEntities[x];
                    }
                }
            }
        }



        return null;
    }


    #region Properties

    public GameObject[] EventPrefabs { get => eventPrefabs; set => eventPrefabs = value; }
    public List<EventPool> EventPools { get => eventPools; set => eventPools = value; }
    public int PrefabNumber { get => prefabNumber; set => prefabNumber = value; }
    public bool HasInstantiatedPrefabs { get => hasInstantiatedPrefabs; set => hasInstantiatedPrefabs = value; }
    public Transform EventHolder { get => eventHolder; set => eventHolder = value; }
    public List<GameObject> Holders { get => holders; set => holders = value; }

    #endregion

}
