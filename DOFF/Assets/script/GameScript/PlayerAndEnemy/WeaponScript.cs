using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class WeaponScript : MonoBehaviour
{
    
    private bool swing = false;
    
    int degree = 0;
    private float weaponY = -0.25f;
    private float weaponX = -0.19f;

    Vector3 pos;
    public GameObject player;
    public ButtonController button; 

    void Update()
    {  
        if(button.GetComponent<ButtonController>().isPressed || Input.GetKey(KeyCode.Space)) 
        {
            GetComponent<SpriteRenderer>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
            Attack();
        }
    }
    

    private void FixedUpdate()
    {
        if(swing)
        {
            if(player.GetComponent<PlayerMovement>().turnedLeft)
            {
                degree += 7; // Swing sword 
            }   
            else
            {
                degree -= 7; // Swing sword 
            }
            
            if(degree > 65 || degree <-65)
            {
                degree = 0;
                swing = false;
                GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
                
            }
            transform.eulerAngles = Vector3.forward * degree;       
        }       
    }

    void Attack()
    {
        if(player.GetComponent<PlayerMovement>().turnedLeft) // When Player facing left
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weaponX = -0.19f; // The Weapon Sprite goes to the left side of the Player
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weaponX = 0.19f; // The Weapon Sprite stays at the right side of the Player
        }
        pos = player.transform.position;
        pos.x += weaponX;
        pos.y += weaponY;
        transform.position = pos;
        swing = true;      
    }
    
}