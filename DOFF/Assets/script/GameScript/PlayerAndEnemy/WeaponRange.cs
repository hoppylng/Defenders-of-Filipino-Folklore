using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRange : MonoBehaviour
{
    private float weaponY = -0.15f; //Difference of the player and weapon sprite y axis
    private float weaponX = -0.3f;
    public Transform firepoint;
    public ButtonController button;
    public GameObject ballShoot;
    Vector3 pos; //To calculate vector position
    public GameObject player; //To access player script
    void Update()
    {
        if(button.GetComponent<ButtonController>().isPressed || Input.GetKey(KeyCode.Space))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        if(player.GetComponent<PlayerController>().turnedLeft) // When Player facing left
        {
            GetComponent<SpriteRenderer>().flipX = true;
            weaponX = -0.3f; // The Weapon Sprite goes to the left side of the Player
            transform.Rotate(0f,180f,0f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            weaponX = 0.3f; // The Weapon Sprite stays at the right side of the Player
        }
        pos = player.transform.position;
        pos.x += weaponX;
        pos.y += weaponY;
        transform.position = pos;
        Instantiate(ballShoot, firepoint.position, firepoint.rotation);
    }
}
