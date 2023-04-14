using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_EnemyProjecter : MonoBehaviour
{
    public static FC_EnemyProjecter EnemyProjecter;
    public List<FC_Enemyfighter> enemies;
    public bool isActive = true;
    public GameObject EnemyfighterPrefab;
    public Transform minPos;
    public Transform maxPos;

    // It might allow timeline script for spawning enemy fighters, 
    // or identify several pattern of fighters.

    // For testing, random spawn pattern with certain interval;
    public float interval = 3; // in sec
    private float timeCount = 0;
    private float difficulty;

    private void Start()
    {
        EnemyProjecter = this;
        enemies = new List<FC_Enemyfighter>();
        difficulty = EventData.GetData().difficulty;

        // Elimination-> 1.125f ~ 2f, Survival-> 1.025f ~ 1.9f
        interval = 1 + (8 - difficulty) / 8 - (EventData.GetData().gameMode == "Survival" ? 0.1f : 0);
    }

    void FixedUpdate()
    {
        if (FC_GameManager.IsGameActive == false) return;

        timeCount += Time.fixedDeltaTime;
        if (timeCount >= interval)
        {
            timeCount = 0;
            FC_Enemyfighter temp = SpawnEnemyfighter();

            // For testing
            temp.speed = Random.Range(600 + difficulty * 20, 700 + difficulty * 20);
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
        FC_Enemyfighter enemy = Instantiate(EnemyfighterPrefab, new Vector2(spawnX, spawnY), Quaternion.identity).GetComponent<FC_Enemyfighter>();
        EnemyProjecter.enemies.Add(enemy);

        return enemy;
    }

    public static void DestroyEnemy(FC_Enemyfighter enemy)
    {
        EnemyProjecter.enemies.Remove(enemy);
        enemy.SelfDestroy();
    }
    public static void RemoveEnemy(FC_Enemyfighter enemy)
    {
        EnemyProjecter.enemies.Remove(enemy);
    }
    public static void DestroyAllEnemy()
    {
        foreach (FC_Enemyfighter enemy in EnemyProjecter.enemies)
        {
            enemy.SelfDestroy();
        }
        EnemyProjecter.enemies.Clear();
    }
}
