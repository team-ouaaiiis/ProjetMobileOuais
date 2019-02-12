using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Monobehaviour Callbacks

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void OnEnable()
    {
        GameManager.instance.RegisterEntity(this);
    }

    public virtual void OnDisable()
    {
        GameManager.instance.UnregisterEntity(this);
    }

    #endregion
}

