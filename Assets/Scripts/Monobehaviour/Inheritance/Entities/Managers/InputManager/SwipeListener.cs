using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeListener : MonoBehaviour
{
    public InputManager inputManager;


    private void Start()
    {
        inputManager = InputManager.inputManager;
        inputManager.OnSwipe += Swipe;
    }

    public void Swipe(InputManager.SwipeDirection _swipeDirection)
    {
        Debug.Log("SWIPE DETECTED IN " + _swipeDirection);
    }

}
