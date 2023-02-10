using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {

    public int EnemySpeed;
    public int XMoveDirection;
    

    // Update is called once per frame
    void Update() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(XMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(XMoveDirection, 0) * EnemySpeed;
        if (hit.distance < 1.9f) {
            Flip();
            if (hit.collider.tag == "Player") {
                Destroy(hit.collider.gameObject);
            }
        }
        //PLAYER DIRECTION
        if (XMoveDirection < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (XMoveDirection > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    void Flip() {
        if (XMoveDirection > 0) {
            XMoveDirection = -1;
        } else {
            XMoveDirection = 1;
        
        }
    }
}
