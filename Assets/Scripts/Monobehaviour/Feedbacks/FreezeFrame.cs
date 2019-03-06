﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrameManager : Manager
{
    [Header("Parameters")]
    private float freezeFrameSpeed = 1f;
    private AnimationCurve freezeFrameCurve;
    private float freezeFrameCompletion = 0f;
    private bool isFreezeFraming = false;

    public override void Update()
    {
        base.Update();
        FreezeFrameUpdate();
    }

    /// <summary>
    /// Slows down time for a short period.
    /// </summary>
    public void StartFreezeFrame(AnimationCurve curve, float speed)
    {
        if(!isFreezeFraming)
        {
            freezeFrameCompletion = 0f;
            isFreezeFraming = true;
            freezeFrameSpeed = speed;
            freezeFrameCurve = curve;
        }
    }

    private void FreezeFrameUpdate()
    {
        if(isFreezeFraming)
        {
            //Debug.Log("FreezeFrame!");
            freezeFrameCompletion += freezeFrameSpeed * Time.unscaledDeltaTime;
            Time.timeScale = freezeFrameCurve.Evaluate(freezeFrameCompletion);

            if(freezeFrameCompletion >= 1)
            {
                isFreezeFraming = false;
                freezeFrameCompletion = 0f;
                Time.timeScale = 1f;
            }
        }
    }

}
