using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class EnemyZigZag : Enemy
{
    #region Fields

    public enum Side
    {
        Left,
        Right,
        Middle
    }

    ChunkManager chunkManager;


    [Header("ZigZag Movement")]
    public Side currentSide;
    public float lineChangeFrequency = 0.5f;
    [Range(0.0f,1.0f)]
    public float lineChangeChance = 0.5f;
    float tick;
    [SerializeField] float smoothTime = 0.25f;
    bool changingLine;
    Vector3 velocityRef;


    [ReadOnly]
    public float leftX, rightX;
    public float targetX;

    #endregion

    #region Monobehaviours Callbacks

    public override void Awake()
    {
        base.Awake();

    }

    public override void Start()
    {
        base.Start();

        chunkManager = GameManager.instance.ChunkManager;

        leftX = -chunkManager.ChunkWidth / 2;
        rightX = chunkManager.ChunkWidth / 2;
    }

    public override void Update()
    {
        base.Update();
        ChangeTick();
        ChangingLine();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        switch (transform.localPosition.x)
        {
            case 2.5f:
                currentSide = Side.Right;
                break;
            case -2.5f:
                currentSide = Side.Left;
                break;
            case 0.0f:
                currentSide = Side.Middle;
                break;
            default:
                currentSide = Side.Middle;
                Debug.LogError("Incorrect position for " + gameObject.name + ", position =  " + transform.localPosition.x);
                break;
        }
    }

    #endregion

    void ChangeTick()
    {
        tick += Time.deltaTime;

        if(tick >= lineChangeFrequency)
        {
            float ran = Random.Range(0.0f, 1.0f);

            if(ran <= lineChangeChance)
            {
                ChangeLine();
            }

            tick = 0;
        }
    }

    [Button]
    public void ChangeLine()
    {
        switch (currentSide)
        {
            case Side.Left:
                targetX = rightX;
                currentSide = Side.Right;
                break;
            case Side.Right:
                targetX = leftX;
                currentSide = Side.Left;
                break;
            case Side.Middle:
                targetX = leftX;
                currentSide = Side.Left;
                break;
        }

        changingLine = true;
    }

    void ChangingLine()
    {
        if (!changingLine) return;

        Debug.Log("Move !!!");
        Vector3 targetPos = new Vector3(targetX, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPos, ref velocityRef, smoothTime);

        if (CustomMethod.AlmostEqualOnOneAxis(transform.localPosition, targetPos, 0.001f, CustomMethod.Axis.X))
        {
            changingLine = false;
        }
    }
}
