using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Enemyfighter : MonoBehaviour
{
    public float speed = 670; // meters per sec, default is the speed of F-22
    public float angularSpeed = 0;
    public float planeAngular = 0;
    public float distanceAngular = 0; // range: -90 <- 0 -> 90
    public bool isUpsideDown = false;
    public FC_SpaceStatus spaceStatus;
    [SerializeField]
    private FC_EnemyStatus enemyStatus;
    public bool isHarmful = true;
    public FC_EnergyShield blockedByIt = null;

    private void Start()
    {
        spaceStatus = GetComponent<FC_SpaceStatus>();
        spaceStatus.RandomInitDistance(2500, 3500);
        enemyStatus = GetComponent<FC_EnemyStatus>();
        spaceStatus.SetFlyOut(false);
    }

    private void FixedUpdate()
    {
        planeAngular += angularSpeed * Time.fixedDeltaTime;
        spaceStatus.Move(speed, planeAngular, distanceAngular);
        spaceStatus.SetUpsideDown(isUpsideDown);
        spaceStatus.AvatarRotationOn();
        if (spaceStatus.GetDistance() < 0)
        {
            HarmfulnessCheck();
            if (isHarmful)
            {
                FC_GameManager.ChangePlayerHP(-1);
            }
            Destroy(this.gameObject);
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
    public void HarmfulnessCheck()
    {
        if (!blockedByIt)
        {

        }
    }



}
