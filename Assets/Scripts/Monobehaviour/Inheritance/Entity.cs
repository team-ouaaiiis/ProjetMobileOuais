using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour, IDamageListener
{
    #region Fields

    [Header("Health")]
    [SerializeField] float healthPoints = 1;
    [HideInInspector] public bool isDead = false;
    float iniHealthPoints;

    [Header("Events")]
    public UnityEvent onTakeDamage;
    public UnityEvent onDeath;

    private Transform holder;

    #endregion

    #region Monobehaviour Callbacks

    public virtual void Awake()
    {
        iniHealthPoints = healthPoints;
    }

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void OnEnable()
    {
        GameManager.instance.RegisterEntity(this);
        isDead = false;
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
        onDeath.Invoke();
;    }

    /// <summary>
    /// Takes a certain amount of damage.
    /// </summary>
    /// <param name="dmg">Amount of damage taken. </param>
    public virtual void TakeDamage(float dmg)
    {
        healthPoints -= dmg;
        onTakeDamage.Invoke();
    }

    public virtual void LaunchedSword()
    {

    }

    public virtual void ResetHealth()
    {
        HealthPoints = iniHealthPoints;
    }

    #endregion

    #region Properties

    public float HealthPoints { get => healthPoints; set => healthPoints = value; }
    public Transform Holder { get => holder; set => holder = value; }

    #endregion
}

public interface IDamageListener
{
    void TakeDamage(float dmg);
}

