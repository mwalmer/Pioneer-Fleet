using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemycruise : MonoBehaviour
{
   public float speed = 5f; // Speed of enemy movement
    public float delay = 2f; // Delay before enemy movement starts
    public Transform player; // Reference to the player's position
    public float radius = 3f; // Distance from player to circle around
    public float angleOffset = 0f; // Angle offset to adjust the circle position
    public float angle = 0f; // Current angle of the circle

    private bool moving = false; // Flag to indicate if enemy is moving
    private Vector3 circlePosition; // Position to circle around

    void Start()
    {
        Invoke("StartMoving", delay); // Start enemy movement after delay

        // Calculate the initial circle position
        Vector2 direction = player.right;
        direction.Normalize();
        Vector3 offset = new Vector3(direction.x, direction.y, 0f) * radius;
        circlePosition = player.position + offset;
        circlePosition = Quaternion.AngleAxis(angleOffset, Vector3.forward) * (circlePosition - player.position) + player.position;
    }

    void Update()
    {
        if (moving)
        {
            // Calculate the circle position based on the current angle
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * radius;
            circlePosition = player.position + offset;

            // Calculate the direction vector towards the circle position
            Vector3 direction = circlePosition - transform.position;

            // Normalize the direction vector
            direction.Normalize();

            // Move the enemy towards the circle position
            transform.position += direction * speed * Time.deltaTime;

            // Rotate the enemy to face the circle position
            float angleToTarget = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angleToTarget, Vector3.forward);

            // Update the current angle
            angle += speed * Time.deltaTime;
        }
    }

    void StartMoving()
    {
        moving = true;
    }
}
