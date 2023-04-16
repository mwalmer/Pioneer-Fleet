using UnityEngine;
using System.Collections;

public class BossFire : MonoBehaviour {

    public GameObject bulletPrefab;
    int bulletLayer;

    public float fireDelay = 0.50f;
    float cooldownTimer = 0;
    public float track;
    Transform player;

    public float dispersionAngle = 30f; // in degrees

    void Start() {
        bulletLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update () {

        if(player == null) {
            // Find the player's ship!
            GameObject go = GameObject.FindWithTag ("Player");
            //Debug.Log ("find");
            if(go != null) {
                player = go.transform;
                Debug.Log ("found");
            }
        }

        cooldownTimer -= Time.deltaTime;

        if( cooldownTimer <= 0 && player != null && Vector3.Distance(transform.position, player.position) < track) {
            // SHOOT!
            // Debug.Log ("Enemy Pew!");
            cooldownTimer = fireDelay;

            Vector3 targetDirection = player.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            angle += Random.Range(-dispersionAngle, dispersionAngle);

            Quaternion rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
            Vector3 offset = rotation * new Vector3(0, 0.5f, 0);

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, rotation);
            bulletGO.layer = bulletLayer;
        }
    }
}
