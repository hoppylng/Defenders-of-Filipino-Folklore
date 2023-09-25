using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]

public class WeaponScript : MonoBehaviour
{
    
    private bool swing = false; //Default to false because it needs to be activated first
    
    int degree = 0; //For the sword animation
    private float weaponY = -10.5f; //Difference of the player and weapon sprite y axis
    private float weaponX = -8.8f; //Difference of the player and weapon sprite x axis
    public float attackSpeed; 
    private float attackTimer;

    Vector3 pos; //To calculate vector position
    public GameObject player; //To access player script
    public ButtonController button; //To access Button script

    void Update()
    {  
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackSpeed && button.GetComponent<ButtonController>().isPressed ||   attackTimer >= attackSpeed && Input.GetKey(KeyCode.Space)) //To activate the attack by pressing the attack button
        {
            GetComponent<SpriteRenderer>().enabled = true; //To activate the weapon sprite because its default to false so it doesn't show on the game it will only get shown if the player click the attack button
            transform.GetChild(0).gameObject.SetActive(true); //This is to activate its child object that is the weapon collision
            Attack();
            attackTimer = 0f; //Reseting the attack speed timer
        } 
    }
    

    private void FixedUpdate()
    {
        if(swing)
        {
            if(player.GetComponent<PlayerController>().turnedLeft)
            {
                degree += 7; // Swing sword to the left
            }   
            else
            {
                degree -= 7; // Swing sword to the right
            }
            
            if(degree > 65 || degree <-65) //Sword swing animation ending point
            {
                degree = 0;
                swing = false;
                GetComponent<SpriteRenderer>().enabled = false; //To in-activate the weapon sprite
                transform.GetChild(0).gameObject.SetActive(false); //To in-activate the weapon collision
                
            }
            transform.eulerAngles = Vector3.forward * degree;  //For the downward animation of the sword
        }      
    }

    void Attack() //This indicate which side of the player the weapon should appear or activated
    {
        
        if(player.GetComponent<PlayerController>().turnedLeft) // When Player facing left
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weaponX = -8.8f; // The Weapon Sprite goes to the left side of the Player
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weaponX = 8.8f; // The Weapon Sprite stays at the right side of the Player
        }
        pos = player.transform.position;
        pos.x += weaponX;
        pos.y += weaponY;
        transform.position = pos;
        swing = true; // activate swing animation
         
    }
    
}