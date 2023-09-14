using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class PlayerController : MonoBehaviour
{
    public float speed; //Player movement speed
    public float groundDist; //Player distance from the ground
    public bool turnedLeft = false;

    public LayerMask terrainLayer;
    private Rigidbody rb;
    public Animator animator;
    public FixedJoystick movementJoystick;
    public ButtonController button;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
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
        if(button.GetComponent<ButtonController>().isPressed || Input.GetKey(KeyCode.Space))
        {
            if(x > 0)
            {
                animator.Play("attackRight");
            }
            else if(x < 0)
            {
                animator.Play("attackLeft");
            }
        }
    }
    
}

