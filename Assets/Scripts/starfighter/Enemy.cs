using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health = 100;
	public int shield = 100;
	public GameObject show;
	public GameObject deathEffect;
	public GameObject shieldEffect;
	public GameObject breakEffect;
	public float leftshift=1;

	public void TakeDamage (int shieldDamage,int damage)
	{
		if(	shield >0	)
		{
			shield -=shieldDamage;
			var v = transform.position; 
			v.x-=leftshift;
			Instantiate(shieldEffect, v, Quaternion.identity);
			if(shield<=0)
				Instantiate(breakEffect, transform.position, Quaternion.identity);
		}
		else
		{
			//Instantiate(shieldEffect, transform.position, Quaternion.identity);
			health -= damage;
		}
		

		if (health <= 0)
		{
			Die();
		}
	}

	void Die ()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		show.SetActive(true);
	}

}
