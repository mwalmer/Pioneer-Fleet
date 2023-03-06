using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_damage_handler : MonoBehaviour
{
    public string source;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject colObj = col.gameObject;

        if (source == "PlayerAttack")
        {
            if (colObj.layer == LayerMask.NameToLayer("Enemy"))
            {
                // TODO:: damage the enemy fighter
            }
            else if (colObj.layer == LayerMask.NameToLayer("EnemyBullet") && source != "torpedo")
            {
                // TODO:: damage the enemy attack
            }
        }
        else if (source == "EnemyAttack")
        {
            if (colObj.layer == LayerMask.NameToLayer("Player"))
            {
                // TODO:: damage the player starfighter
            }
            else if (colObj.layer == LayerMask.NameToLayer("PlayerAttack"))
            {
                // TODO:: damage the player attack
            }
        }
    }
}
