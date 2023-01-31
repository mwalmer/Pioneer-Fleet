using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalShip : MonoBehaviour
{
    public int maxShield;
    public int currentShield;
    public int maxHull;
    public int currentHull;
    public int artilleryPower;
    public int flakPower; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int enemyArtillery){
        if(currentShield > 0){
            currentShield = currentShield - enemyArtillery;
            if(currentShield < 0){
                currentHull = currentHull + currentShield;
                currentShield = 0;
        }
        }
        if(currentShield <= 0){
            currentHull = currentHull - enemyArtillery;
            currentShield = 0;
        }
    }
}
