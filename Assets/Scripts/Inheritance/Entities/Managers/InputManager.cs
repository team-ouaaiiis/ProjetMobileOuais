using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Manager
{ 
    public override void Start()
    {
        base.Start();
    }


    public override void Update()
    {
        base.Update();
    }

    public bool TapOnScreen()
    {
        if(Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
