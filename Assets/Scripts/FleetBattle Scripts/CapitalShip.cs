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
    SpriteRenderer sprite;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setup(){
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = Color.blue;
    }

    public void takeDamage(int enemyArtillery){
        if(currentShield > 0){
            currentShield = currentShield - enemyArtillery;
            if(currentShield < 0){
                sprite.color = Color.white;
                currentHull = currentHull + currentShield;
                currentShield = 0;
        }
        }
        if(currentShield <= 0){
            sprite.color = Color.red;
            currentHull = currentHull - enemyArtillery;
            currentShield = 0;
            if(currentHull <= 0){
                sprite.color = Color.black;
            }
        }
    }
}
