using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_EnemyProjecter : MonoBehaviour
{
    public GameObject EnemyfighterPrefab;
    public Transform minPos;
    public Transform maxPos;

    // It might allow timeline script for spawning enemy fighters, 
    // or identify several pattern of fighters.

    // For testing, random spawn pattern with certain interval;
    public float interval = 3; // in sec
    private float timeCount = 0;


    void FixedUpdate()
    {
        timeCount += Time.fixedDeltaTime;
        if (timeCount >= interval)
        {
            timeCount = 0;
            SpawnEnemyfighter();
        }
    }

    void SpawnEnemyfighter()
    {
        if (minPos == null || maxPos == null)
            return;
        //TODO:: 
        //1. random a position in the camera
        float spawnX = Random.Range(minPos.position.x, maxPos.position.x);
        float spawnY = Random.Range(minPos.position.y, maxPos.position.y);

        //2. instantiate the enemy fighter and spwan into the right position from the above.
        Instantiate(EnemyfighterPrefab, new Vector2(spawnX, spawnY), Quaternion.identity);
    }
}
