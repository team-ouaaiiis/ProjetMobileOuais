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

    [Header("Pinch")]
    [SerializeField] private float minDistanceBetweenFingers = 10f;
    [SerializeField] private float maxDistanceBetweenFingers = 50f;

    [Header("Position")]
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private float minSens = 0.2f;
    [SerializeField] private float maxSens = 1f;
    [SerializeField] private float smoothCam = 0.2f;
    [SerializeField] private float zoomAmnt = 10f;
    private Vector3 currentVelCam;
    private bool isMoving = false;
    private Vector2 initialPosPointer;
    private Vector3 initialPosMap;
    private float scroll = 0f;
    private float targetSize;

    [Header("Double Tap")]
    [SerializeField] private float waitForDoubleTapTime = 0.08f;
    [SerializeField] private float waitForTapRelase = 0.08f;
    [SerializeField] private bool waitingForTapRelease = false;
    [SerializeField] private bool waitingForDoubleTap = false;
    private Coroutine IWaitingForTapRelease;
    private Coroutine IWaitingForDoubleTap;

    //Zoom
    [SerializeField] private bool hasTwoFingers = false;
    private float initialDistance;
    private float initialScroll;
    

    private void Update()
    {
        Position();
        Inputs();
        ZoomManager();
    }

    private void ZoomManager()
    {
        scroll = Mathf.Clamp(scroll, 0, 1);
        targetSize = Mathf.Lerp(minCamSize, maxCamSize, scroll);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, targetSize, ref currentVelSmooth, scrollSmooth);       

#if UNITY_EDITOR

        scroll -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * scrollSpeed;

#endif

#if UNITY_ANDROID
        if (hasTwoFingers)
        {
            float dist = Vector3.Distance(TouchPos(0), TouchPos(1)) - initialDistance;
            float t = dist + Mathf.Lerp(maxDistanceBetweenFingers, 0, initialScroll);            
            scroll = Mathf.InverseLerp(maxDistanceBetweenFingers, 0, t);            
            Debug.Log(scroll);
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
            target.transform.position = initialPosMap + diff * sensitivity * Mathf.Lerp(minSens, maxSens, scroll);
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
                OnSecondInputReleased(false);
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

        if (waitingForDoubleTap)
        {
            waitingForDoubleTap = false;

            if (scroll < 0.5)
            {
                scroll = 1;
            }

            else
            {
                scroll = 0;
            }
        }

        else
        {
            waitingForTapRelease = true;
            if (IWaitingForTapRelease != null) StopCoroutine(IWaitingForTapRelease);
            IWaitingForTapRelease = StartCoroutine(WaitingForTapRelease());
        }
    }

    private IEnumerator WaitingForTapRelease()
    {
        yield return new WaitForSeconds(waitForTapRelase);
        waitingForTapRelease = false;
    }

    /// <summary>
    /// Called when releasing the main Input (first finger or mouse click)
    /// </summary>
    /// <param name="editor"></param>
    private void OnMainInputReleased(bool editor)
    {
        isMoving = false;

        if(waitingForTapRelease)
        {
            waitingForTapRelease = false;
            waitingForDoubleTap = true;
            if (IWaitingForDoubleTap != null) StopCoroutine(IWaitingForDoubleTap);
            IWaitingForDoubleTap = StartCoroutine(WaitingForDoubleTap());
        }        
    }

    private IEnumerator WaitingForDoubleTap()
    {
        yield return new WaitForSeconds(waitForTapRelase);
        waitingForDoubleTap = false;
    }

    /// <summary>
    /// Called when clicking with the secondary Input (second finger or debug Key)
    /// </summary>
    /// <param name="editor"></param>
    private void OnSecondInputClicked(bool editor)
    {

        if (editor)
        {

        }

        else
        {
            initialDistance = Vector3.Distance(TouchPos(0), TouchPos(1));
            initialScroll = scroll;
            hasTwoFingers = true;
            //scroll = Mathf.InverseLerp(minCamSize, maxCamSize, cam.orthographicSize);
        }
    }

    /// <summary>
    /// Called when releasing the secondary Input (second finger or debug Key)
    /// </summary>
    /// <param name="editor"></param>
    private void OnSecondInputReleased(bool editor)
    {
        hasTwoFingers = false;
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

    private Vector3 WorldTouchPos(int i)
    {
        if (Input.touchCount > 0)
        {
            Vector3 touch = new Vector3(Input.GetTouch(i).position.x, Input.GetTouch(i).position.y, transform.position.z);
            return Camera.main.ScreenToWorldPoint(touch);
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
