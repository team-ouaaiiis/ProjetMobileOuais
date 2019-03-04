using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CamBehaviour : Entity
{
    [Header("Components")]
    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] private float xTilt = 50f;
    [SerializeField] private float yPos = 10f;

    public override void Start()
    {
        base.Start();
        PositionCam();
    }

    [Button("Position Cam")]
    private void PositionCam()
    {
        cam.transform.localEulerAngles = new Vector3(xTilt, cam.transform.localEulerAngles.y, cam.transform.localEulerAngles.z);
        cam.transform.position = new Vector3(cam.transform.position.x, yPos, cam.transform.position.z);
    }
}
