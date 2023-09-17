using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) //To check if it collides to an object with enemy tag on it
        {
            collision.gameObject.GetComponent<EnemyScript>().TakeDamage(2); //When the object that have enemy tag take damage when it get hit
        }
        
    }
}
