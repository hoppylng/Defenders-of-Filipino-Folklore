using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float range;
    public Transform target; //Enemy going towards
    private Rigidbody rb;
    private Animator animator;
    private float minDistance = 5.0f; //If the player get outside of this range it doesnt follow 
    private bool targetCollision = false;
    private float speed = 2.0f; //Enemy base movement speed
    private float Push = 1.5f; //How long it will get push
    public int EnemyHealth = 6; //Enemy health
    private int hitdamage = 10; //Enemy damage to the player health
    public Behaviour script; //To disabling a script

    public Sprite deathSprite; //Temporary death animation
    //public Sprite[] sprites;
    private bool isDead = false; //For checking if the its still alive

    void Start()
    {
        //int rnd = Random.Range(0,sprites.Length);
        //GetComponent<SpriteRenderer>().sprite = sprites[rnd]; 
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        range = Vector2.Distance(transform.position, target.position); //To calculate how for the position to the player
        if(range < minDistance && !isDead) // if range of enemy towards the player is less than the minimum distance, and if the sprite is still alive its true the enemy go towards the enemy
        {
            if(!targetCollision)
            {
                transform.LookAt(target.position); //Enemy identify and goes towards the target
                 if (target.position.x < transform.position.x) //To check if the target is on its left or right
                {
                    animator.Play("left"); //If the target is towards the left this get activate
                }
                else
                {
                    animator.Play("right"); //If the target is towards the right this get activate 
                }
                transform.Rotate(new Vector3(0,-90,0),Space.Self);
                transform.Translate(new Vector3(speed* Time.deltaTime,0,0));
            }
        }
        transform.rotation = Quaternion.identity; //To calculate the rotation of the object
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player") && !targetCollision) //To check if it collides to an object with player tag on it
        {
            Vector3 contactPoint = collision.contacts[0].point; //To save the contact point of the enemy and the player
            Vector3 center = collision.collider.bounds.center; //To calculate the center point of the collision

            targetCollision = true; //Indicate that it collide

            bool right = contactPoint.x > center.x; //If it collides came from the right side
            bool left = contactPoint.x < center.x; //If it collides came from the left side
            bool top = contactPoint.y > center.y; //If it collides came from the top side
            bool bottom = contactPoint.y < center.y; //If it collides came from the bottom side

            //This if statements just indicate where the enemy get push/bounce away from the player 
            if(right) 
            {
                GetComponent<Rigidbody>().AddForce(transform.right * Push, ForceMode.Impulse); 
            }
            if(left)
            {
                GetComponent<Rigidbody>().AddForce(-transform.right * Push, ForceMode.Impulse); 
            }
            if(top) 
            {
                GetComponent<Rigidbody>().AddForce(transform.up * Push, ForceMode.Impulse);
            } 
            if(bottom) 
            {
                GetComponent<Rigidbody>().AddForce(-transform.up * Push, ForceMode.Impulse); 
            }
            Invoke("FalseCollision",0.5f); //Animation of the push/bounce
        }
    }

    void FalseCollision() //After the collision the vector of the enemy get reset to zero
    {
        targetCollision = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void TakeDamage(int amount) //Getting hit by the player weapon
    {
        EnemyHealth -= amount; //If it get hit enemy health gets lower
        transform.GetChild(0).gameObject.SetActive(true); //This activate the blood sprite
        if(EnemyHealth<0) //To check if enemy is still alive or not
        {
            GetComponent<SpriteRenderer>().sprite = deathSprite; //temporary death sprithe
            isDead = true; //To indicate that the enemy is dead
            GetComponent<Rigidbody>().velocity = Vector3.zero; //To make sure that it doesnt slide/move still when it dies
            transform.GetChild(0).gameObject.SetActive(false); //To in-activate the blood
            animator.SetTrigger("death");
            script.enabled = false; //To disable this script
            GetComponent<Collider>().enabled = false; //To make sure that it doesnt collide with another entity even its dead
            
            Invoke("EnemyDeath",1.5f); //Enemy animation
            
        }
        
        Invoke("HideBlood",0.25f); //Animation of the blood to indicate that it got hit
    }
    void HideBlood() //To in-activate the blood
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void EnemyDeath() //To remove the enemy sprite from the stage
    {
        Destroy(gameObject);
    }

    public int GetHitDamage() //Public function that return the enemy damage
    {
        return hitdamage;
    }
}
