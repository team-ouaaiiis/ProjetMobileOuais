using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFeedback : Entity
{
    public override void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayFeedback();
        }
    }
}
