using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class LevelManager : Manager
{
    [SerializeField] AnimationCurve chunkSpeedEvolution;
    [SerializeField, MinMaxSlider(1.0f, 20.0f)] Vector2 speedAmplitude = new Vector2(5,15);
    float minSpeed, maxSpeed;
    [SerializeField, Tooltip("Time (in seconds) to reach the maximum speed")] float timeToReachMaxSpeed = 500;
    [SerializeField,ReadOnly] float countdown;
    ChunkManager chunkManager;


    public override void Awake()
    {
        base.Awake();
        minSpeed = speedAmplitude.x;
        maxSpeed = speedAmplitude.y;
    }

    public override void Start()
    {
        base.Start();
        chunkManager = GameManager.instance.ChunkManager;
    }

    public override void Update()
    {
        base.Update();
        Timer();
        SpeedAdjustement();
    }


    void Timer()
    {
        countdown += Time.deltaTime;
    }

    void SpeedAdjustement()
    {
        float percent = countdown / timeToReachMaxSpeed;
        percent = Mathf.Clamp(percent, 0.0f, 1.0f);
        float speedEvaluation = chunkSpeedEvolution.Evaluate(percent);
        float speed = Mathf.Lerp(minSpeed, maxSpeed, speedEvaluation);
        chunkManager.ScrollSpeed = speed;
    }

}
