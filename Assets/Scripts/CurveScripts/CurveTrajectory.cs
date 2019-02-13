using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class CurveTrajectory : MonoBehaviour
{
    [BoxGroup("Color settings")]
    public Color curveColor = Color.white;
    [BoxGroup("Color settings")]
    public Color controlPointsColor = Color.green;

    //Has to be at least 4 so-called control points
    [Required]
    public Transform startPoint;
    [Required]
    public Transform endPoint;
    [Required]
    public Transform controlPointStart;
    [Required]
    public Transform controlPointEnd;

    [ReadOnly]
    [ShowIf("CurveConfigured")]
    [BoxGroup("Positions")]
    public Vector3 startPosition, startControlPosition, endControlPosition, endPosition;

    public bool CurveConfigured()
    {
        if (startPoint == null || endPoint == null || controlPointStart == null || controlPointEnd == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    void OnDrawGizmos()
    {

        GetPositions();

        //Set curve's color
        Gizmos.color = curveColor;

        //The start position of the line
        Vector3 lastPos = startPosition;

        //The resolution of the line
        float resolution = 0.02f; //0.02 c'est bien, faut pas faire le fou avec cette variable

        //How many loops?
        int loops = Mathf.FloorToInt(1f / resolution);

        for (int i = 1; i <= loops; i++)
        {
            float t = i * resolution;

            
            Vector3 newPos = BezierCurvePoint(t); //Find positions between the control points

            
            Gizmos.DrawLine(lastPos, newPos); //Draw as a new segment

            
            lastPos = newPos; //Save this pos pour draw the next segment
        }

        //lines between controlPoints and endPoints
        Gizmos.color = controlPointsColor;

        Gizmos.DrawLine(startPosition, startControlPosition);
        Gizmos.DrawLine(endControlPosition, endPosition);
    }

    void GetPositions()
    {
        startPosition = startPoint.position;
        startControlPosition = controlPointStart.position;
        endControlPosition = controlPointEnd.position;
        endPosition = endPoint.position;
    }


    public Vector3 BezierCurvePoint(float t)
    {
        GetPositions();
        
        //To make it faster
        float oneMinusT = 1f - t;

        //Layer 1
        Vector3 Q = oneMinusT * startPosition + t * startControlPosition;
        Vector3 R = oneMinusT * startControlPosition + t * endControlPosition;
        Vector3 S = oneMinusT * endControlPosition + t * endPosition;

        //Layer 2
        Vector3 P = oneMinusT * Q + t * R;
        Vector3 T = oneMinusT * R + t * S;

        //Final interpolated position
        Vector3 U = oneMinusT * P + t * T;

        return U;
    }

    [Button("Create Curve")]
    public void CreateBezierCurve()
    {
        //start point creation
        GameObject startPointObject = new GameObject("Start Point");
        startPointObject.transform.SetParent(transform);
        startPointObject.transform.localPosition = new Vector3(-1, 0, 0);
        startPointObject.AddComponent<CurveHandle>();
        startPoint = startPointObject.transform;   
        
        //end point creation
        GameObject endPointObject = new GameObject("End Point");
        endPointObject.transform.SetParent(transform);
        endPointObject.transform.localPosition = new Vector3(1, 0, 0);
        endPointObject.AddComponent<CurveHandle>();
        endPoint = endPointObject.transform;  
        
        //control start point creation
        GameObject controlStartPointObject = new GameObject("Control Start Point");
        controlStartPointObject.transform.SetParent(transform);
        controlStartPointObject.transform.localPosition = new Vector3(-1.5f, 1, 0);
        controlStartPointObject.AddComponent<CurveHandle>();
        controlPointStart = controlStartPointObject.transform;  
        
        //control start point creation
        GameObject controlEndPointObject = new GameObject("Control End Point");
        controlEndPointObject.transform.SetParent(transform);
        controlEndPointObject.transform.localPosition = new Vector3(1.5f, 1, 0);
        controlEndPointObject.AddComponent<CurveHandle>();
        controlPointEnd = controlEndPointObject.transform;

    }

    [Button("Delete curve")]
    public void DeleteCurve()
    {
        Transform[] childs = gameObject.GetComponentsInChildren<Transform>();

        for (int i = 0; i < childs.Length; i++)
        {
            if(childs[i].gameObject != gameObject)
                DestroyImmediate(childs[i].gameObject);
        }

        startPosition = Vector3.zero;
        startControlPosition = Vector3.zero;
        endControlPosition = Vector3.zero;
        endPosition = Vector3.zero;

    }

}
