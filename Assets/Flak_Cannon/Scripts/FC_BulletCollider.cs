using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_BulletCollider : MonoBehaviour
{
    FC_CannonBullet cBullet;
    FC_Enemyfighter closestEnemy = null;
    public float currentDistance = 0;
    public float explodingDistance = 100f;
    bool wasFront = false; // use to check if bullet speed is higher than exploding distance;
    // Start is called before the first frame update
    void Start()
    {
        cBullet = this.GetComponentInParent<FC_CannonBullet>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (cBullet.IsBulletActive() == false) return;

    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (cBullet.IsBulletActive() == false) return;
        if (closestEnemy != null)
        {
            Debug.Log(Mathf.Abs(closestEnemy.GetDistance() - cBullet.GetDistance()));
        }
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            FC_Enemyfighter temp = col.GetComponentInParent<FC_Enemyfighter>();
            if (temp == null)
            {
                return;
            }

            if (Mathf.Abs(temp.GetDistance() - cBullet.GetDistance()) <= explodingDistance)
            {
                cBullet.Explode(temp);
            }
            else if (cBullet.GetDistance() < temp.GetDistance() && cBullet.GetDistance() + cBullet.GetSpeed() * Time.fixedDeltaTime > temp.GetDistance())
            {
                cBullet.Explode(temp);
            }
            else if (cBullet.GetDistance() > temp.GetDistance())
            {
                cBullet.Explode(temp);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (cBullet.IsBulletActive() == false) return;

    }

}
