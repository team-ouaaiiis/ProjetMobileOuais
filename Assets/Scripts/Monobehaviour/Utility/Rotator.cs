using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotSpeed = 10f;
    void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * rotSpeed, 0, 0));
    }
}
