using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
	public int damage = 40;
	public Rigidbody2D rb;


	// Use this for initialization
	void Start () {
		rb.velocity = transform.right * speed;
	}

	void OnTriggerEnter2D (Collider2D hitInfo)
	{
		EnemyScript enemy = hitInfo.GetComponent<EnemyScript>();
		if (enemy != null)
		{
			enemy.TakeDamage(damage);
		}

		

		Destroy(gameObject);
	}
}
