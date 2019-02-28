using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject target;
    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float smoothCam = 0.2f;
    [SerializeField] private float zoomAmnt = 10f;
    private Vector3 currentVelCam;
    private bool isMoving = false;
    private Vector2 initialPosPointer;
    private Vector3 initialPosMap;
    private float initialDistance;

    private void Update()
    {
        Position();
        MouseDetection();
    }

    private void Position()
    {
        Vector3 diff = new Vector3();
#if UNITY_EDITOR
        diff = (new Vector3(ConvertedMousePos().x, 0, ConvertedMousePos().y) - new Vector3(initialPosPointer.x, 0, initialPosPointer.y));
#endif

#if UNITY_ANDROID
        if(Input.touchCount > 0 )
            diff = (new Vector3(TouchPos(0).x, 0, TouchPos(0).y) - new Vector3(initialPosPointer.x, 0, initialPosPointer.y));

        if (Input.touchCount >= 2)
        {
            cam.orthographicSize = 50 + (1f / (initialDistance - Vector3.Distance(TouchPos(0), TouchPos(1))) * zoomAmnt);
        }
#endif

        if (isMoving)
        {
            target.transform.position = initialPosMap + diff * sensitivity;
        }
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target.transform.position, ref currentVelCam, smoothCam);
    }

    private void MouseDetection()
    {
#if UNITY_ANDROID

        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isMoving = true;
                initialPosPointer = TouchPos(0);
                initialPosMap = target.transform.position;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                isMoving = false;
            }
            
            if (Input.touchCount > 1)
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    initialDistance = Vector3.Distance(TouchPos(0), TouchPos(1));
                }
            }
        }

        

#endif

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            initialPosPointer = ConvertedMousePos();
            initialPosMap = target.transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

 #endif
    }

    private Vector3 TouchPos(int i)
    {
        if(Input.touchCount > 0)
        {
            return new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, transform.position.z);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 ConvertedMousePos()
    {
        return Input.mousePosition;
    }
}
