using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_CannonBullet : MonoBehaviour
{
    public float damage = 10;
    public Vector2 targetPos;
    private Vector2 initialPos;
    private Vector2 intervalDistance; // in second
    private FC_SpaceStatus spaceStatus;
    [SerializeField]
    float speed;
    [SerializeField]
    bool isBulletActive = false;


    // Self-destroy timer
    float sdCountdown = 10; // in sec


    // Start is called before the first frame update
    void Start()
    {
        spaceStatus = GetComponent<FC_SpaceStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        // The movement of a bullet in screen's horizontal and vertical space
        this.transform.position = new Vector3(transform.position.x + intervalDistance.x * Time.deltaTime,
                                            transform.position.y + intervalDistance.y * Time.deltaTime,
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
        Debug.Log(targetPos);
        // Get targeting distance and interval for every sec
        intervalDistance = new Vector2((targetPos.x - initialPos.x) / screenMovingModifer, (targetPos.y - initialPos.y) / screenMovingModifer);
    }

    void CollisionCheck()
    {

    }

    void Explode(FC_Enemyfighter eFighter)
    {
        eFighter.TakeDamage(damage);
    }

}
