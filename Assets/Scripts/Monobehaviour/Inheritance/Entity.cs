﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Entity : MonoBehaviour, IDamageListener
{
    #region Fields

    [SerializeField] private bool hasHealth = false;
    
    [ShowIf("hasHealth")]
    [SerializeField] float healthPoints = 1;
    [HideInInspector] public bool isDead = false;
    float iniHealthPoints;

    [Header("Events")]

    [ShowIf("hasHealth")]
    public UnityEvent onTakeDamage;
    [ShowIf("hasHealth")]
    public UnityEvent onDeath;

    [HideInInspector][SerializeField] private List<Feedback> feedbacks = new List<Feedback>();
    private Transform holder;
    private bool isInitialized = false;
    private Chunk parentChunk;

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
        if(parentChunk != null)
        {
            parentChunk.ChunkElements.Remove(this);
            parentChunk = null;
        }
        GameManager.instance.UnregisterEntity(this);
    }

    public virtual void SwapCharacter()
    {

    }

    public virtual void ShopBubbleSpawn()
    {

    }

    public virtual void ShopBubbleDespawn()
    {

    }

    #endregion

    #region Private Methods

    //private void HealthManager()
    //{
    //    if(healthPoints <= 0 && !isDead)
    //    {
    //        Debug.Log("Dead");
    //        isDead = true;
    //        healthPoints = 0;
    //    }
    //}

    #endregion

    #region Public Methods

    public virtual void Initialize(bool initialize)
    {
        IsInitialized = initialize;
    }

    [ContextMenu("Add Feedback")]
    public virtual void AddFeedback()
    {
        Feedback newFeedback = gameObject.AddComponent<Feedback>();
        Feedbacks.Add(newFeedback);
    }

    [ContextMenu("Remove all Feedbacks")]
    public virtual void RemoveAllFeedbacks()
    {
        if(feedbacks.Count > 0)
        {
            for (int i = 0; i < feedbacks.Count; i++)
            {
                DestroyImmediate(feedbacks[i]);
            }

            feedbacks.Clear();
        }
    }

    public virtual void PlayFeedback(string name)
    {
        for (int i = 0; i < Feedbacks.Count; i++)
        {
            if (Feedbacks[i].FeedbackName == name)
            {
                Feedbacks[i].PlayFeedback();
                break;
            }
        }
    }

    public virtual void PlayFeedback()
    {
        Feedbacks[0].PlayFeedback();
    }

    public virtual void Death()
    {
        isDead = true;
        onDeath.Invoke();
    }

    public virtual void OnGameOver()
    {

    }

    /// <summary>
    /// Takes a certain amount of damage.
    /// </summary>
    /// <param name="dmg">Amount of damage taken. </param>
    public virtual void TakeDamage(float dmg)
    {
        healthPoints -= dmg;
        onTakeDamage.Invoke();
    }

    #endregion

    #region Callbacks

    #region Gameplay

    public virtual void ResetHealth()
    {
        HealthPoints = iniHealthPoints;
    }

    public virtual void PlayerWeaponHitEntity()
    {

    }

    #endregion

    #region Feedbacks

    public virtual void Shake(float amnt, float dur)
    {
        
    }

    public virtual void Zoom(AnimationCurve curve, float speed, bool shouldStay)
    {

    }

    public virtual void FreezeFrame(AnimationCurve curve, float speed)
    {

    }
    
    public virtual void Blink(Color col, Renderer mat, int count, float delay, float time)
    {

    }

    public virtual void ShakeObject(GameObject toShake, AnimationCurve curve, float intensity, float speed, Space space, Vector3 shakeAxes)
    {

    }

    #endregion

    #region UI

    public virtual void ShopBubbleSelection(int iD)
    {

    }

    public virtual void ShopRackMove(bool isRight)
    {

    }

    #endregion

    #endregion

    #region Properties

    public float HealthPoints { get => healthPoints; set => healthPoints = value; }
    public Transform Holder { get => holder; set => holder = value; }
    public List<Feedback> Feedbacks { get => feedbacks; set => feedbacks = value; }
    public bool HasHealth { get => hasHealth; }
    public bool IsInitialized { get => isInitialized; set => isInitialized = value; }

    public Chunk ParentChunk { get => parentChunk; set => parentChunk = value; }

    #endregion
}

public interface IDamageListener
{
    void TakeDamage(float dmg);
}

