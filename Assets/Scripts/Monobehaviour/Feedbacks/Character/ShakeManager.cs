using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectShake
{
    #region Fields

    [Header("Shake")]
    [SerializeField] private GameObject objectToShake;
    [SerializeField] private AnimationCurve curveShake;
    [SerializeField] private float intensityShake;
    [SerializeField] private float speedShake;
    [SerializeField] private Space space;
    [SerializeField] private Vector3 axes;
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
    public Space Space { get => space; set => space = value; }
    public Vector3 Axes { get => axes; set => axes = value; }

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
   
    public override void ShakeObject(GameObject toShake, AnimationCurve curve, float intensity, float speed, Space space, Vector3 shakeAxes)
    {
        base.ShakeObject(toShake, curve, intensity, speed, space, shakeAxes);
        bool canShake = true;

        for (int i = 0; i < shakes.Count; i++)
        {
            if(shakes[i].ObjectToShake == toShake)
            {
                Debug.Log("Already Shaking this object");
                canShake = false;
                break;
            }
        }

        if(canShake)
        {
            ObjectShake newObjectShake = new ObjectShake();

            newObjectShake.IsShaking = true;
            newObjectShake.ShakeProgress = 0f;
            newObjectShake.ObjectToShake = toShake;
            newObjectShake.CurveShake = curve;
            newObjectShake.IntensityShake = intensity;
            newObjectShake.SpeedShake = speed;
            newObjectShake.Space = space;
            newObjectShake.Axes = shakeAxes;

            switch (space)
            {
                case Space.Local:
                    newObjectShake.InitialPos = newObjectShake.ObjectToShake.transform.localPosition;
                    break;

                case Space.World:
                    newObjectShake.InitialPos = newObjectShake.ObjectToShake.transform.position;
                    break;
            }

            shakes.Add(newObjectShake);
        }
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

                    switch (shk.Space)
                    {
                        case Space.Local:
                            shk.ObjectToShake.transform.localPosition = shk.InitialPos + CustomMethod.MultiplyTwoVectors(Random.insideUnitSphere, shk.Axes) * mul;
                            break;

                        case Space.World:
                            shk.ObjectToShake.transform.position = shk.InitialPos + CustomMethod.MultiplyTwoVectors(Random.insideUnitSphere, shk.Axes) * mul;
                            break;
                    }
                }

                if (shk.ShakeProgress >= 1f)
                {
                    switch (shk.Space)
                    {
                        case Space.Local:
                            shk.ObjectToShake.transform.localPosition = shk.InitialPos;
                            break;

                        case Space.World:
                            shk.ObjectToShake.transform.position = shk.InitialPos;
                            break;
                    }

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
            if (!shakes[i].IsShaking)
            {
                shakes.RemoveAt(i);
            }
        }
    }
}
