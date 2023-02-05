using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {


    [SerializeField] private GameObject projectile;
    private bool facingRight = true;
    [SerializeField] private int MaxActiveShots;
    private Rigidbody2D rbdPlayer;
    GameObject shot;
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
         //       transform.RotateAround(rbdPlayer.transform.position, Vector3.up, 180f);
                facingRight = true;
            }
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            transform.RotateAround(rbdPlayer.transform.position, Vector3.up, 180f);
            facingRight = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (GameObject.FindGameObjectsWithTag("Attack").Length < MaxActiveShots)
            {
                Instantiate(projectile, this.transform.position, this.transform.rotation);
                audioSource.clip = audioClip;
                audioSource.Play();
                //shot.transform.position = this.transform.position;
                //shot.SetActive(true);
                //float playerSpeed = rbdPlayer.velocity.x;
                //shot.GetComponent<Projectileship>().Launch(playerSpeed, true);
                //Destroy(shot, 5f);
            }
        }

    }
}
