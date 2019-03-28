using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject target;
    [SerializeField] private Camera cam;

    [Header("Zoom")]
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private float scrollSmooth = 0.2f;
    [SerializeField] private float minCamSize = 20f;
    [SerializeField] private float maxCamSize = 120f;
    private float currentVelSmooth;

    [Header("Settings")]
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float smoothCam = 0.2f;
    [SerializeField] private float zoomAmnt = 10f;
    private Vector3 currentVelCam;
    private bool isMoving = false;
    private Vector2 initialPosPointer;
    private Vector3 initialPosMap;
    private float scroll = 0f;

    //Zoom
    [SerializeField] private bool hasTwoFingers = false;
    private float initialDistanceBetweenTwoFingers;
    private float initialCamSize = 20f;

    private void Start()
    {
        initialCamSize = cam.orthographicSize;
    }

    private void Update()
    {
        Position();
        Inputs();
        ZoomManager();
    }

    private void ZoomManager()
    {

#if UNITY_EDITOR
        scroll += Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed;
        float targetSize = initialCamSize - scroll;
        targetSize = Mathf.Clamp(targetSize, minCamSize, maxCamSize);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetSize, ref currentVelSmooth, scrollSmooth);
#endif

#if UNITY_ANDROID
        if (hasTwoFingers)
        {


        }
#endif
    }

    private void Position()
    {
        Vector3 diff = new Vector3();
#if UNITY_EDITOR
        diff = (new Vector3(MousePos().x, 0, MousePos().y) - new Vector3(initialPosPointer.x, 0, initialPosPointer.y));
#endif

#if UNITY_ANDROID
        if (Input.touchCount > 0)
            diff = (new Vector3(TouchPos(0).x, 0, TouchPos(0).y) - new Vector3(initialPosPointer.x, 0, initialPosPointer.y));

#endif

        if (isMoving && !hasTwoFingers)
        {
            target.transform.position = initialPosMap + diff * sensitivity;
        }
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target.transform.position, ref currentVelCam, smoothCam);
    }

    private void Inputs()
    {
#if UNITY_ANDROID

        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began) //Started moving
            {
                OnMainInputClicked(false);
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended) //Stopped moving
            {
                OnMainInputReleased(false);
            }

            if (Input.touchCount > 1) // Second Finger -> Zoom
            {
                if (Input.GetTouch(1).phase == TouchPhase.Began) //On Second touch, getting the distance between fingers
                {
                    OnSecondInputClicked(false);

                }

                if (Input.GetTouch(1).phase == TouchPhase.Ended) // Released second touch, stop zoom
                {
                    OnSecondInputReleased(false);
                }
            }
        }

#endif

#if UNITY_EDITOR


        if (Input.GetMouseButtonDown(0))
        {
            OnMainInputClicked(true);

        }

        if (Input.GetMouseButtonUp(0))
        {
            OnMainInputReleased(true);
            OnSecondInputReleased(true);
        }
#endif
    }

    /// <summary>
    /// Called when clicking with the main Input (first finger or mouse click)
    /// </summary>
    /// <param name="editor"></param>
    private void OnMainInputClicked(bool editor)
    {
        isMoving = true;
        initialPosMap = target.transform.position;

        if (editor)
        {
            initialPosPointer = MousePos();
        }

        else
        {
            initialPosPointer = TouchPos(0);
        }
    }

    /// <summary>
    /// Called when releasing the main Input (first finger or mouse click)
    /// </summary>
    /// <param name="editor"></param>
    private void OnMainInputReleased(bool editor)
    {
        isMoving = false;
    }

    /// <summary>
    /// Called when clicking with the secondary Input (second finger or debug Key)
    /// </summary>
    /// <param name="editor"></param>
    private void OnSecondInputClicked(bool editor)
    {
        initialCamSize = cam.orthographicSize;

        if (editor)
        {

        }

        else
        {
            initialDistanceBetweenTwoFingers = Vector3.Distance(TouchPos(0), TouchPos(1));
        }
    }

    /// <summary>
    /// Called when releasing the secondary Input (second finger or debug Key)
    /// </summary>
    /// <param name="editor"></param>
    private void OnSecondInputReleased(bool editor)
    {

    }

    #region Returns

    private Vector3 TouchPos(int i)
    {
        if (Input.touchCount > 0)
        {
            return new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, transform.position.z);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 MousePos()
    {
        return Input.mousePosition;
    }

    private Vector3 WorldMousePos()
    {
        return Camera.main.ScreenToWorldPoint(MousePos());
    }

    #endregion
}
