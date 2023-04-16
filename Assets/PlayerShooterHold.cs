using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterHold : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    private bool facingRight = true;
    [SerializeField] private int maxActiveShots;
    private Rigidbody2D rbdPlayer;
    public float firingCooldown = 0.1f;
    private float lastFiredTime;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Use this for initialization
    void Start()
    {
        rbdPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
        {
            if (Input.GetAxis("Horizontal") < 0)
            {
                //transform.RotateAround(rbdPlayer.transform.position, Vector3.up, 180f);
                facingRight = true;
            }
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.RotateAround(rbdPlayer.transform.position, Vector3.up, 180f);
            facingRight = true;
        }

        if (Input.GetMouseButton(0) && Time.time >= lastFiredTime + firingCooldown)
        {
            if (GameObject.FindGameObjectsWithTag("Attack").Length < maxActiveShots)
            {
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                float angle = Random.Range(-80f, -100f);
                projectile.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                audioSource.clip = audioClip;
                audioSource.Play();
                lastFiredTime = Time.time;
            }
        }
    }
}
