using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_Cannon : MonoBehaviour
{
    public Transform leftCannon;
    public Transform rightCannon;
    public float offset = 38f;
    public Transform cursorLocation;

    public FC_CannonBullet bullet;
    public float firePower = 1000;
    public float fireInterval = 0.7f;
    private float fireCharge = 0;
    public Transform leftMuzzle;
    public Transform rightMuzzle;
    public Transform bulletCollector;


    private void Update()
    {
        AdjustCannonAngles();
        DetectOperation();
    }

    void AdjustCannonAngles()
    {
        // Adjust Left
        float oppo = cursorLocation.position.y - leftCannon.position.y;
        float adj = cursorLocation.position.x - leftCannon.position.x;
        float angle = Mathf.Rad2Deg * Mathf.Atan(oppo / adj);
        leftCannon.eulerAngles = Vector3.forward * (angle - offset);

        // Adjust Right
        oppo = cursorLocation.position.y - rightCannon.position.y;
        adj = cursorLocation.position.x - rightCannon.position.x;
        angle = Mathf.Rad2Deg * Mathf.Atan(oppo / adj);
        rightCannon.eulerAngles = Vector3.forward * (angle + offset);
    }

    void DetectOperation()
    {
        // Fire
        if (fireCharge <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Fire();
                fireCharge = fireInterval;
            }
        }
        else
        {
            fireCharge -= Time.deltaTime;
        }

        // Sheild


        // ETC.


    }
    void Fire()
    {
        FC_CannonBullet b = (FC_CannonBullet)Instantiate(bullet, leftMuzzle.position, Quaternion.identity, bulletCollector);
        b.InitBullet(cursorLocation.position, firePower);
        b = (FC_CannonBullet)Instantiate(bullet, rightMuzzle.position, Quaternion.identity, bulletCollector);
        b.InitBullet(cursorLocation.position, firePower);
    }
}
