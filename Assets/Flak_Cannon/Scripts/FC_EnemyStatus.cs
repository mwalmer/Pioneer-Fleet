using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_enemyStatus : MonoBehaviour
{
    float hp = 10;
    float def = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        EnemyUpdate();
    }

    void EnemyUpdate()
    {
        //TODO :: needs to be override by inheritance.
    }

    public void TakeDamage(float dmg)
    {
        hp = hp - (def > 0 ? dmg / (1 + def) : dmg);
        LifeCheck();
    }

    void LifeCheck()
    {
        if (hp <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        Destroy(this.gameObject);
    }

}
