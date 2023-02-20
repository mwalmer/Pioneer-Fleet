using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_CannonBullet : MonoBehaviour
{
    public float damage = 10;
    public Vector2 targetPos;
    private Vector2 initialPos;
    private Vector2 intervalDistance; // in second
    private float depthVisualModifer = 1.09f;
    private FC_SpaceStatus spaceStatus;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isBulletActive = false;

    // Collision Check
    private FC_Enemyfighter closestEnemy = null;
    private float explodingRange = 100f;
    public ParticleSystem explosion = null;

    // Self-destroy timer
    float sdCountdown = 3; // in sec


    // Start is called before the first frame update
    void Start()
    {
        spaceStatus = GetComponent<FC_SpaceStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (explosion != null && explosion.gameObject.active)
        {
            if (explosion.isStopped)
            {
                Destroy(this.gameObject);
            }
        }


        if (!isBulletActive)
            return;
        /*
        // The movement of a bullet in screen's horizontal and vertical space
        float newX = transform.position.x + intervalDistance.x * Time.deltaTime * depthVisualModifer;
        if (Mathf.Abs(transform.position.x - targetPos.x) < (newX - targetPos.x))
        {
            newX = targetPos.x;
        }

        float newY = transform.position.y + intervalDistance.y * Time.deltaTime * depthVisualModifer;
        if (Mathf.Abs(transform.position.y - targetPos.y) < (newY - targetPos.y))
        {
            newY = targetPos.y;
        }
        this.transform.position = new Vector3(newX, newY, transform.position.z);
        */

        this.transform.position = new Vector3(
                                    transform.position.x + (targetPos.x - transform.position.x) / 0.125f * Time.deltaTime,
                                    transform.position.y + (targetPos.y - transform.position.y) / 0.125f * Time.deltaTime,
                                    transform.position.z
        );
    }
    void FixedUpdate()
    {
        if (!isBulletActive)
            return;

        spaceStatus.Move(speed, true);
        sdCountdown -= Time.fixedDeltaTime;
        if (sdCountdown < 0)
            Destroy(this.gameObject);
    }
    public void InitBullet(Vector2 _targetPos, float _speed)
    {
        targetPos = _targetPos;
        speed = _speed;
        isBulletActive = true;
        initialPos = this.transform.position;

        float screenMovingModifer = (speed > 0 ? FC_SpaceStatus.rangeOfVisibility / speed : 10);

        // Get targeting distance and interval for every sec
        intervalDistance = new Vector2((targetPos.x - initialPos.x) / screenMovingModifer, (targetPos.y - initialPos.y) / screenMovingModifer);
    }

    public void Explode(FC_Enemyfighter eFighter)
    {
        eFighter.TakeDamage(damage);
        ExplodingEvent();
    }
    void ExplodingEvent()
    {
        // TODO treat events during and after bullet is exploding

        isBulletActive = false;
        spaceStatus.VisualDisable();
        if (explosion != null)
        {
            explosion.gameObject.SetActive(true);
            explosion.Play();
        }
    }

    public float GetDistance()
    {
        return spaceStatus.GetDistance();
    }

    public bool IsBulletActive()
    {
        return isBulletActive;
    }
    public float GetSpeed()
    {
        return speed;
    }

}
