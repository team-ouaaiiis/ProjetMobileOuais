using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IDamageListener
{
    #region Fields

    [Header("Health")]
    [SerializeField] private float healthPoints = 1;
    private bool isDead = false;

    private Transform holder;

    #endregion

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

    #region Private Methods

    private void HealthManager()
    {
        if(healthPoints <= 0 && !isDead)
        {
            
        }
    }

    #endregion

    #region Public Methods

    public virtual void Death()
    {
        isDead = true;
    }

    /// <summary>
    /// Takes a certain amount of damage.
    /// </summary>
    /// <param name="dmg">Amount of damage taken. </param>
    public virtual void TakeDamage(float dmg)
    {
        healthPoints -= dmg;
    }

    public virtual void LaunchedSword()
    {

    }

    #endregion

    #region Properties

    public float HealthPoints { get => healthPoints; }
    public Transform Holder { get => holder; set => holder = value; }

    #endregion
}

public interface IDamageListener
{
    void TakeDamage(float dmg);
}

