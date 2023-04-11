using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_EnemyStatus : MonoBehaviour
{
    [SerializeField]
    float hp = 10;
    [SerializeField]
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
    }

    public bool LifeCheck()
    {
        if (hp <= 0)
        {
            return false;
        }
        return true;
    }

    public void Dead()
    {
        FC_GameManager.CountDestroy();
        Destroy(this.gameObject);
    }

}
