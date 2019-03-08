using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Enums

public enum Space
{
    Local,
    World
}

#endregion

public class Shake : Entity
{
    #region Fields
    [Header("Components")]
    [SerializeField] [Tooltip("Leave null to shake this object.")] private GameObject objectToShake;

    [Header("Space")]
    [SerializeField] private Space space;

    [Header("Filter Axis")]
    [SerializeField] private bool x = true;
    [SerializeField] private bool y = true;
    [SerializeField] private bool z = true;

    #endregion

    public override void Start()
    {
        base.Start();
        if(objectToShake == null)
        {
            objectToShake = this.gameObject;
        }
    }

    public float BoolToInt(bool b)
    {
        if (b)
        {
            return 1;
        }

        else
        {
            return 0;
        }
    }

    #region Properties

    public bool X { get => x; set => x = value; }
    public bool Y { get => y; set => y = value; }
    public bool Z { get => z; set => z = value; }
    public Space Space { get => space; set => space = value; }
    public GameObject ObjectToShake { get => objectToShake; set => objectToShake = value; }

    #endregion
}
