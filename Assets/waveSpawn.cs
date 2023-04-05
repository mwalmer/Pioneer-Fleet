using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawn : MonoBehaviour
{
    public GameObject objectToSpawn;    // The prefab of the object to spawn
    public int numberOfObjects = 5;     // The number of objects to spawn
    public float spawnDelay = 10f;       // The delay between spawns in seconds
    public float minY = 0f;             // The minimum Y position for spawning
    public float maxY = 5f;             // The maximum Y position for spawning

    private float timer = 0f;           // The timer to keep track of spawn delay

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if the timer exceeds the spawn delay
        if (timer >= spawnDelay)
        {
            // Reset the timer
            timer = 0f;

            // Spawn the specified number of objects
            for (int i = 0; i < numberOfObjects; i++)
            {
                // Generate a random Y position within the specified range
                float spawnPosY = Random.Range(minY, maxY);
                // Generate a random position within the spawn area (2D)
                Vector3 spawnPos = new Vector3(transform.position.x, spawnPosY, transform.position.z);
                // Spawn the object with a 90-degree rotation around the Z-axis
                GameObject spawnedObject = Instantiate(objectToSpawn, spawnPos, Quaternion.Euler(0f, 0f, 90f));
            }
        }
    }
}