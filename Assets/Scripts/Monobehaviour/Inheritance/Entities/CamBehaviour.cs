using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CamBehaviour : Entity
{
    [Header("Components")]
    [SerializeField] private Camera cam;
    [SerializeField] private CameraShake camShake;
    [SerializeField] private CamZoom zoom;

    [Header("Settings")]
    [SerializeField] private float xTilt = 50f;
    [SerializeField] private float yPos = 10f;

    #region Monobehaviour Callbacks

    public override void Start()
    {
        base.Start();
        PositionCam();
    }

    #endregion

    #region Entity Callbacks

    public override void Shake(float amnt, float dur)
    {
        base.Shake(amnt, dur);
        camShake.Shake(dur, amnt);
    }

    public override void Zoom(AnimationCurve curve, float speed, bool shouldStay)
    {
        base.Zoom(curve, speed, shouldStay);
        zoom.TriggerCamZoom(curve, speed, shouldStay);
    }

    #endregion

    [Button("Position Cam")]
    private void PositionCam()
    {
        cam.transform.localEulerAngles = new Vector3(xTilt, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
        cam.transform.position = new Vector3(cam.transform.position.x, yPos, cam.transform.position.z);
    }
}
