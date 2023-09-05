using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonController : MonoBehaviour
{
    public bool isPressed;
    public void PointerDown()
    {
        isPressed = true;
        
    }
    public void PointerUp()
    {
        isPressed = false;
    }
}
