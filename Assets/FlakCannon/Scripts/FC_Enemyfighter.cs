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
    public GameObject enemyExplosion;

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
            SpwanExplosionEffect();
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (enemyStatus)
        {
            enemyStatus.TakeDamage(dmg);
            if (enemyStatus.LifeCheck() == false)
            {
                SpwanExplosionEffect();
                enemyStatus.Dead();
            }
        }
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

    public void SpwanExplosionEffect()
    {
        ParticleSystem explosion = Instantiate(enemyExplosion).GetComponent<ParticleSystem>();
        explosion.transform.localScale = new Vector3(transform.localScale.x / 10f, transform.localScale.y / 10f, transform.localScale.z / 10f);
        explosion.transform.position = transform.position;
        ParticleSystemRenderer explosionRender = explosion.GetComponent<ParticleSystemRenderer>();
        explosionRender.sortingLayerName = "objects";
        explosionRender.sortingOrder = (int)(spaceStatus.GetDistance() + 1f);
    }

}
