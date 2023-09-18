using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed; //Player movement speed
    public float groundDist; //Player distance from the ground
    public bool turnedLeft = false;
    public int PlayerMaxHealth = 100; //Player max health
    public int currentHealth; //Variable that updates current health of the player

    public LayerMask terrainLayer;
    private Rigidbody rb;
    public Animator animator;
    public FixedJoystick movementJoystick;
    public Behaviour script; //To disable a script
    public Behaviour scripta;
    public Behaviour scriptb;
    public Behaviour scriptc;
    
    public HealthBarScript healthBar; //Health bar to show how much health of the player have
    public Text mainText; //Temporary Game Over screen
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currentHealth = PlayerMaxHealth; 
        healthBar.SetMaxHealth(PlayerMaxHealth); 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if(Physics.Raycast(castPos, -transform.up,out hit, Mathf.Infinity, terrainLayer)) //This is to check if the ground the player moving have a hill or not
        {
            if(hit.collider !=null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }
        
        //THIS IS FOR KEYBOARD INPUT IF YOU GUYS WANT TO TRY JUST COMMENT OUT THE FIXED UPDATE AND UNCOMMENT THIS
        /*moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        moveInput.Normalize();
        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, moveInput.y *speed);

        

        if(moveInput.x < 0)
        {
            animator.Play("left");
            
        }
        else if(moveInput.x > 0)
        {
            animator.Play("right");
            
        }*/
        
    }
    void FixedUpdate()
    {
        float x = movementJoystick.Horizontal; //To get the x axis of the joystick
        float z = movementJoystick.Vertical; //To get the y axis of the joystick
        Vector3 direction = new Vector3(x,0,z).normalized; //This just normalize the value of x and y so its not a bunch of numbers
        transform.Translate(direction *speed, Space.World); //This make the sprite/player move
        turnedLeft = false;
        if(x > 0) //If the joystick moving towards the right side
        {
            animator.Play("right"); //Plays the animation
    
        }
        else if(x < 0) //If the joystick moving towards the left side
        {
            animator.Play("left"); //Plays the animation
            turnedLeft = true;
            
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))//If the player and enemy collide this get trigger
        {
            transform.GetChild(0).gameObject.SetActive(true); //this just indicate that the player got hit so a blood sprite get activated
            currentHealth -= collision.gameObject.GetComponent<EnemyScript>().GetHitDamage(); //Calculation for the player current health if it got hit by the enemy
            if(currentHealth < 1)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero; //This just makes the player sprite not slide when it dies mid movement animation
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

