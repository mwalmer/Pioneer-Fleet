using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_EnemyProjecter : MonoBehaviour
{
    public bool isActive = true;
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
        if (FC_GameManager.IsGameActive == false) return;

        timeCount += Time.fixedDeltaTime;
        if (timeCount >= interval)
        {
            timeCount = 0;
            FC_Enemyfighter temp = SpawnEnemyfighter();

            // For testing
            temp.speed = Random.Range(650, 800);
            temp.planeAngular = Random.Range(-180, 180);
            temp.angularSpeed = Random.Range(10, 90);
            temp.distanceAngular = Random.Range(0, 90);
            if (Random.Range(0, 100) < 50)
            {
                temp.isUpsideDown = true;
            }
        }
    }

    FC_Enemyfighter SpawnEnemyfighter()
    {
        if (minPos == null || maxPos == null)
            return null;
        //TODO:: 
        //1. random a position in the camera
        float spawnX = Random.Range(minPos.position.x, maxPos.position.x);
        float spawnY = Random.Range(minPos.position.y, maxPos.position.y);

        //2. instantiate the enemy fighter and spwan into the right position from the above.
        return Instantiate(EnemyfighterPrefab, new Vector2(spawnX, spawnY), Quaternion.identity).GetComponent<FC_Enemyfighter>();
    }
}
