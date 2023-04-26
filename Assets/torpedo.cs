using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class torpedo : MonoBehaviour
{

    public float velocity = 10f;
    public float eventualspeed = 60f;
    public float accTime = 3f;
    public int shieldDamage = 40;
    public int damage = 40;

    //	public Rigidbody2D rb;
    public GameObject impactEffect;
    public float angle;
    float currentV;

    float currentY;
    public float accerateTimeY = 1f;
    public float eY = 0f;
    public float veloY = 10f;
    // Rigidbody m_Rigidbody;
    // Use this for initialization
    void Start()
    {
        currentV = velocity;
        currentY = veloY;
    }
    void Update()
    {
        Vector3 newPos = transform.position;
        currentV = Mathf.Lerp(currentV, eventualspeed, accTime * Time.deltaTime);
        newPos.x += currentV * Time.deltaTime;
        //(float)transform.rotation.y*(float).5
        currentY = Mathf.Lerp(currentY, eY, accerateTimeY * Time.deltaTime);
        newPos.y += currentY * Time.deltaTime;
        transform.position = newPos;
        //	Debug.Log(currentY);
        //	 m_Rigidbody.AddForce(transform.up * acceleration);

    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {


        Enemy enemy = hitInfo.GetComponent<Enemy>(); //
        if (enemy == false)
            return;
        if (enemy.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (enemy != null)
            {
                enemy.TakeDamage(shieldDamage, damage);
            }
            if (impactEffect) Instantiate(impactEffect, transform.position, transform.rotation);
            foreach (Transform child in transform)
            {
                //Debug.Log("TEST" + child.gameObject.name);
                Destroy(child.gameObject);
            }
            //	Debug.Log("TEST2" + gameObject.name);
            Destroy(gameObject);

        }

    }

}
