using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	public int health = 1;
	int maxHealth;
	public int damage=1;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	public bool isPlayer = false;
	public Image healthBar;
	public int Energy =0;

	SpriteRenderer spriteRend;

	void Start() {
		correctLayer = gameObject.layer;
		maxHealth = health;
		// NOTE!  This only get the renderer on the parent object.
		// In other words, it doesn't work for children. I.E. "enemy01"
		spriteRend = GetComponent<SpriteRenderer>();

		if(spriteRend == null) {
			spriteRend = transform.GetComponentInChildren<SpriteRenderer>();

			if(spriteRend==null) {
				Debug.LogError("Object '"+gameObject.name+"' has no sprite renderer.");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D hitInfo) {

		GameObject bullet = hitInfo.gameObject;
			if(bullet.layer==LayerMask.NameToLayer("torpedo")) //this is added for all bullets to not be damaged by torpedos, they shouldn't
		{
	//	Debug.Log("ddd");
		}
		else if(isPlayer)
		{health--;}
		else
		{health--; }
	  
		//health--;
        if (isPlayer)
        {
			
			UpdateHealth();
        }

		if(invulnPeriod > 0) {
			invulnTimer = invulnPeriod;
			gameObject.layer = 10;
		}
		
	}

	void Update() {

		if(invulnTimer > 0) {
			invulnTimer -= Time.deltaTime;

			if(invulnTimer <= 0) {
				gameObject.layer = correctLayer;
				if(spriteRend != null) {
					spriteRend.enabled = true;
				}
			}
			else {
				if(spriteRend != null) {
					spriteRend.enabled = !spriteRend.enabled;
				}
			}
		}

		if(health <= 0) {
			Die();
		}
	}

	void Die() {
		Destroy(gameObject);
	}

	void UpdateHealth()
    {
		healthBar.fillAmount = (float)health / (float)maxHealth;
    }

}
