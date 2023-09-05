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
        public int PlayerMaxHealth = 100;
        public int currentHealth;
        public Behaviour script;
        public Behaviour scripta;
        public Behaviour scriptb;
        public Behaviour scriptc;
        public HealthBarScript healthBar;
        public Text mainText;
        private PlayerMovement disable;

        void Start()
        {
            disable =GetComponent<PlayerMovement>();
            rb = GetComponent<Rigidbody2D>();   
            animator = GetComponent<Animator>();
            currentHealth = PlayerMaxHealth;
            healthBar.SetMaxHealth(PlayerMaxHealth);
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
                currentHealth -= collision.gameObject.GetComponent<EnemyScript>().GetHitDamage();
                if(currentHealth < 1)
                {
                    mainText.gameObject.SetActive(true);
                    Die();
                    mainText.text = "Game Over";
                }
                healthBar.SetHealth(currentHealth);
                Invoke("HidePlayerBlood", 0.25f);
            }
        }

        void HidePlayerBlood()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void Die()
        {
            script.enabled = false;
            scripta.enabled = false;
            scriptb.enabled = false;
            scriptc.enabled = false;
            animator.SetTrigger("death");
        }
    }