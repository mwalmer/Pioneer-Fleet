using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFighter : MonoBehaviour
{

    public int EnginePower;
    public int Accuracy;
    public int BombingPower;
    public bool deadge;
    public bool damadge;
    //star fighters take 1 on 1s with the enemy. Leftover starfighters deal damage to capital ships
    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeFire(int enemyAccuracy){
        int hitChance = EnginePower - enemyAccuracy;
        if(hitChance < Random.Range(0,100)){
            hit();
        }
    }

    public void hit(){
        if(60 < Random.Range(0,100)){
            deadge = true;
        }
        else {
            damadge = true;
        }
    }

}
