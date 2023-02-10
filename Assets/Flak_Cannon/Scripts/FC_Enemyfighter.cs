using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Enemyfighter : MonoBehaviour
{
    public float speed = 670; // meters per sec, default is the speed of F-22
    public FC_SpaceStatus spaceStatus;
    [SerializeField]
    private FC_EnemyStatus enemyStatus;

    private void Start()
    {
        spaceStatus = GetComponent<FC_SpaceStatus>();
        spaceStatus.RandomInitDistance(2500, 3500);
        enemyStatus = GetComponent<FC_EnemyStatus>();
    }

    private void FixedUpdate()
    {
        spaceStatus.Move(speed);
        if (spaceStatus.GetDistance() < 0)
        {
            FC_GameManager.ChangePlayerHP(-1);
            gameObject.SetActive(false);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (enemyStatus)
            enemyStatus.TakeDamage(dmg);
    }
    public float GetDistance()
    {
        return spaceStatus.GetDistance();
    }

}
