using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Enemyfighter : MonoBehaviour
{
    public float speed = 670; // meters per sec, default is the speed of F-22
    public FC_SpaceStatus spaceStatus;
    [SerializeField]
    private FC_enemyStatus enemyStatus;

    private void Start()
    {
        spaceStatus = GetComponent<FC_SpaceStatus>();
        spaceStatus.RandomInitDistance(2500, 3500);
        enemyStatus = GetComponent<FC_enemyStatus>();
    }

    private void FixedUpdate()
    {
        spaceStatus.Move(speed);
        if (spaceStatus.GetDistance() < 0)
            gameObject.SetActive(false);
    }

    public void TakeDamage(float dmg)
    {
        if (enemyStatus)
            enemyStatus.TakeDamage(dmg);
    }

}
