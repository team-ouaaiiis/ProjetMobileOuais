using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Fields

    public static GameManager instance;
    private List<Entity> entities = new List<Entity>();
    private BiomeManager biomeManager;    

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        Initialize();
    }

    [ContextMenu("Initialize")]
    private void Initialize()
    {
        instance = this;
    }

    #endregion

    #region Public Methods

    public void RegisterEntity(Entity entity)
    {
        Entities.Add(entity);
    }

    public void UnregisterEntity(Entity entity)
    {
        Entities.Remove(entity);
    }

    public void LaunchedSword()
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].LaunchedSword();
        }
    }

    #endregion

    #region Properties

    public List<Entity> Entities { get => entities; }
    public BiomeManager BiomeManager { get => biomeManager; set => biomeManager = value; }

    #endregion
}
