using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveHandle : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "CurveHandle");
    }
}
