using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDebug : MonoBehaviour
{
    [Range(0.1f,2.0f)]
    public float timeScale = 1;
    public bool debugTime;
    public KeyCode increase, decrease;


    public void Update()
    {
        if (debugTime)
        {
            if (Input.GetKeyDown(increase))
            {
                ChangeTimeScale(0.25f);
            }

            if (Input.GetKeyDown(decrease))
            {
                ChangeTimeScale(-0.25f);
            }

            Time.timeScale = timeScale;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ChangeTimeScale(float _addValue)
    {
        timeScale += _addValue;
        timeScale = Mathf.Clamp(timeScale, 0.0f, 2.0f);
    }
}
