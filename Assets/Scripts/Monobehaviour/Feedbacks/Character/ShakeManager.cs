using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObjectShake
{
    #region Fields

    [Header("Test Shake")]
    [SerializeField] private GameObject objectToShake;
    [SerializeField] private AnimationCurve curveShake;
    [SerializeField] private float intensityShake;
    [SerializeField] private float speedShake;
    private float shakeProgress;
    private bool isShaking;
    private Vector3 initialPos;

    #endregion

    #region Properties

    public GameObject ObjectToShake { get => objectToShake; set => objectToShake = value; }
    public AnimationCurve CurveShake { get => curveShake; set => curveShake = value; }
    public float IntensityShake { get => intensityShake; set => intensityShake = value; }
    public float SpeedShake { get => speedShake; set => speedShake = value; }
    public bool IsShaking { get => isShaking; set => isShaking = value; }
    public Vector3 InitialPos { get => initialPos; set => initialPos = value; }
    public float ShakeProgress { get => shakeProgress; set => shakeProgress = value; }

    #endregion
}

public class ShakeManager : Manager
{
    
    #region Fields

    [SerializeField] private List<ObjectShake> shakes = new List<ObjectShake>();

    #endregion

    public override void Update()
    {
        base.Update();
        ShakeUpdate();
        DestroyShakes();
    }

    public void ShakeObject(GameObject toShake, AnimationCurve curve, float intensity, float speed)
    {
        ObjectShake newObjectShake = new ObjectShake();

        newObjectShake.IsShaking = true;
        newObjectShake.ShakeProgress = 0f;
        newObjectShake.ObjectToShake = toShake;
        newObjectShake.CurveShake = curve;
        newObjectShake.IntensityShake = intensity;
        newObjectShake.SpeedShake = speed;
    }

    private void ShakeUpdate()
    {
        for (int i = 0; i < shakes.Count; i++)
        {
            ObjectShake shk = shakes[i];
            if (shk.IsShaking)
            {
                if (shk.ShakeProgress < 1f)
                {
                    shk.ShakeProgress += shk.SpeedShake * Time.deltaTime;
                    float mul = shk.CurveShake.Evaluate(shk.ShakeProgress) * shakes[i].IntensityShake;
                    shk.ObjectToShake.transform.position = shk.InitialPos + Random.insideUnitSphere * mul;
                }

                if(shk.ShakeProgress >= 1f)
                {
                    shk.ObjectToShake.transform.position = shk.InitialPos;
                    shk.IsShaking = false;                    
                }
            }

            shakes[i] = shk;
        }
    }

    private void DestroyShakes()
    {
        for (int i = 0; i < shakes.Count; i++)
        {
            if(!shakes[i].IsShaking)
            {
                shakes.RemoveAt(i);
            }
        }
    }
    
}
