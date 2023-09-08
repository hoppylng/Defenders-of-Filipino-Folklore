    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerMovement : MonoBehaviour
    {
    
        public JoystickMovement movementJoystick; //Access to the joystick script
        public float playerSpeed; //Player Movement speed
        private Rigidbody2D rb;
        private Animator animator;
        public bool turnedLeft = false; //To indicate if the player is facing left or not
        public int PlayerMaxHealth = 100; //Player max health
        public int currentHealth; //Variable that updates current health of the player
        public Behaviour script; //To disable a script
        public Behaviour scripta;
        public Behaviour scriptb;
        public Behaviour scriptc;
        public HealthBarScript healthBar; //Health bar to show how much health of the player have
        public Text mainText; //Temporary Game Over screen

        void Start()
        { 
            rb = GetComponent<Rigidbody2D>();   
            animator = GetComponent<Animator>();
            currentHealth = PlayerMaxHealth; 
            healthBar.SetMaxHealth(PlayerMaxHealth); 
        }
    
      
        void FixedUpdate()
        {
            if(movementJoystick.joystickVec.y != 0) //If its use this argument becomes true
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed); //To identify the joystick position and move towards
            }
            else
            {
                rb.velocity = Vector2.zero; //If its not getting use it goes back to its original place
            }
            turnedLeft = false; 
            if(movementJoystick.joystickVec.x > 0) //If the joystick moving towards the right side
            {
                animator.Play("right");
            }
            else if(movementJoystick.joystickVec.x < 0) //If the joystick moving towards the left side
            {
                animator.Play("left");
                turnedLeft = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Enemy"))//If the player and enemy collide this get trigger
            {
                transform.GetChild(0).gameObject.SetActive(true); //this just indicate that the player got hit so a blood sprite get activated
                currentHealth -= collision.gameObject.GetComponent<EnemyScript>().GetHitDamage(); //Calculation for the player current health if it got hit by the enemy
                if(currentHealth < 1)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector3.zero; //This just makes the player sprite not slide when it dies mid movement animation
                    mainText.gameObject.SetActive(true); //This show the Game over text on the screen
                    Die();
                    mainText.text = "Game Over";
                }
                healthBar.SetHealth(currentHealth); 
                Invoke("HidePlayerBlood", 0.25f); //this indicate how long the blood sprite last
            }
        }

        void HidePlayerBlood() //To hide the blood sprite
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void Die() //to disable the movement in the game when the player die
        {
            script.enabled = false;
            scripta.enabled = false;
            scriptb.enabled = false;
            scriptc.enabled = false;
            animator.SetTrigger("death");
        }
    }