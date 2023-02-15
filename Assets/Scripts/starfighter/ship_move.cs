using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_move : MonoBehaviour
{
    public float speed = 20f;
    private float moveX;
    private float moveY;
    //private Vector3 moveDir;

    void Update()
    {
        Vector3 pos = transform.position;

        //Currently, player can manoever his spaceship either with w,s,a,d or arrow keys. Another constraint was added so that the player can manoever his ship inside the game board.
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && pos.y <= 4.2)
        {
            pos.y += speed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && pos.y >= -4.2)
        {
            pos.y -= speed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && pos.x <= 8)
        {
            pos.x += speed * Time.deltaTime;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && pos.x >= -8)
        {
            pos.x -= speed * Time.deltaTime;
        }


        transform.position = pos;

        //moveX = Input.GetAxis("Horizontal");
        //moveY = Input.GetAxis("Vertical");
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveY * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.x);

        //Vector3 moveDir = new Vector3(moveX, moveY).normalized;

    }

    //Need to work on the collision between two spaceships so that, upon collision, the player's ship's hp will decrease.
    /*
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("TriggerEnter!");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("CollisionEnter!");
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("CollisionExit!");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExit!");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("CollisionStay!");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("TriggerStay!");
    }
    */

}
