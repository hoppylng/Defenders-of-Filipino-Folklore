using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class JoystickMovement : MonoBehaviour
{

    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    private Vector2 joystickTouchPos;
    private Vector2 joystickOriginalPos;
    private float joystickRadius;

    void Start()
    {
        joystickOriginalPos = joystickBG.transform.position; 
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4; //Maximum radius of the joystick can move
    }

    public void PointerDown() //When the joystick get press this indicate to location or the position where it got press
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);

        if(joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void PointerUp() //If the joystick is not getting press the joystick gets back to its original place
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = joystickOriginalPos;
        joystickBG.transform.position = joystickOriginalPos;
    }

}
