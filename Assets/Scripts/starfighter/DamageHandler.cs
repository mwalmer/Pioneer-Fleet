using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageHandler : MonoBehaviour {

	public int health = 1;
	int maxHealth;
	public float invulnPeriod = 0;
	float invulnTimer = 0;
	int correctLayer;
	public bool isPlayer = false;
	public Image healthBar;

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

	void OnTriggerEnter2D() {
		health--;
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
