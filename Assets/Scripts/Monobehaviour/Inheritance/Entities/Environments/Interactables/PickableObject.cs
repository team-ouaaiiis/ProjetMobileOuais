using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : Interactable, IPickableObject
{
    [Header("Pickable object values")]
    public bool isTaken;

    #region Monobehaviour Callbacks

    public override void OnEnable()
    {
        base.OnEnable();
        isTaken = false;
        
    }

    #endregion

    #region Public Methods

    public virtual void GetObject()
    {
        if (isTaken) return;
        isTaken = true;
        gameObject.SetActive(false);
    }

    #endregion
}
