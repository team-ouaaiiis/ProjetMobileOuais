using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    private bool isMoving = false;
    private Vector2 initialPos;

    private void Update()
    {
        Position();
        MouseDetection();
    }

    private void Position()
    {        
        if(isMoving)
        {
            transform.position += new Vector3(ConvertedTouchPos().x, 0, ConvertedTouchPos().z) - new Vector3(initialPos.x, 0, initialPos.y);
        }
    }

    private void MouseDetection()
    {
        #if UNITY_ANDROID

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            isMoving = true;
            initialPos = ConvertedTouchPos();
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            isMoving = false;
        }

        #endif

        #if UNITY_EDITOR

        if(Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            initialPos = ConvertedMousePos();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

#endif

    }

    private Vector3 ConvertedTouchPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    }

    private Vector3 ConvertedMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
