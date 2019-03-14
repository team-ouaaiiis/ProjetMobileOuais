using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackManager : Manager
{
    public static FeedbackManager instance;
    private List<Entity> entities = new List<Entity>();

    public override void Awake()
    {
        base.Awake();
        instance = this;
        entities = GameManager.instance.Entities;
    }

    #region Callbacks

    public void CallShake(float amnt, float duration)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Shake(amnt, duration);
        }
    }

    public void CallZoom(AnimationCurve curve, float speed, bool shouldStay)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Zoom(curve, speed, shouldStay);
        }
    }

    public void CallFreezeFrame(AnimationCurve curve, float speed)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].FreezeFrame(curve, speed);
        }
    }

    public void CallBlink(Color col, Renderer mat, int count, float delay, float time)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].Blink(col, mat, count, delay, time);
        }
    }

    public virtual void CallShakeObject(GameObject toShake, AnimationCurve curve, float intensity, float speed, Space space, Vector3 axes)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].ShakeObject(toShake, curve, intensity, speed, space, axes);
        }
    }

    #endregion
}
