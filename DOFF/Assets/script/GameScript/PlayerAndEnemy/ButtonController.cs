using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour
{
    public bool isPressed = false;
    public void PointerDown() //If the button got pressed
    {
        isPressed = true;
        
    }
    public void PointerUp() //If the button is not getting press
    {
        isPressed = false;
    }
}
