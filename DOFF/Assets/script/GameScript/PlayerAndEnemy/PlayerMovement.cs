    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class PlayerMovement : MonoBehaviour
    {
    
        public JoystickMovement movementJoystick;
        public float playerSpeed;
        private Rigidbody2D rb;
        private Animator animator;
        public bool turnedLeft = false;
        public float health = 100;
        
        public Image healthfill;
        private float healthWidth;
        public Text mainText;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();   
            animator = GetComponent<Animator>();
            healthWidth = healthfill.sprite.rect.width;       
        }
    
      
        void FixedUpdate()
        {
            if(movementJoystick.joystickVec.y != 0)
            {
                rb.velocity = new Vector2(movementJoystick.joystickVec.x * playerSpeed, movementJoystick.joystickVec.y * playerSpeed);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
            turnedLeft = false;
            if(movementJoystick.joystickVec.x > 0)
            {
                animator.Play("right");
            }
            else if(movementJoystick.joystickVec.x < 0)
            {
                animator.Play("left");
                turnedLeft = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.CompareTag("Enemy"))
            {
                transform.GetChild(0).gameObject.SetActive(true);
                health -= collision.gameObject.GetComponent<EnemyScript>().GetHitDamage();
                if(health < 1)
                {
                    healthfill.enabled = false;
                    mainText.gameObject.SetActive(true);
                    animator.Play("death");
                    mainText.text = "Game Over";
                }
                Vector2 temp = new Vector2(healthWidth * (health/100), 40);
                healthfill.rectTransform.sizeDelta = temp;
                Invoke("HidePlayerBlood", 0.25f);
            }
        }

        void HidePlayerBlood()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }