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
    public bool active = true;
    //star fighters take 1 on 1s with the enemy. Leftover starfighters deal damage to capital ships
    //Start is called before the first frame update
    public bool inFlight = false;
    public Transform target;
    SpriteRenderer sprite;
    Color color;
    void Start()
    {
        active = true;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inFlight == true){
            var step =  2.0f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        /*
        if(Vector3.Distance(target.position, transform.position)<0.1f && inFlight == true){
                if(deadge == true){
                    //expode
                }
                if(deadge == false){
                    //target.transform.position = (new Vector3(transform.position.x+2,transform.position.y +1,transform.position.z));
                }
            }
            */
    }
    public void setup(){
        active = true;
        sprite = GetComponent<SpriteRenderer>();
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
            sprite.color = Color.red;
        }
        else {
            damadge = true;
            sprite.color = Color.black;
        }
    }

    public void dogFightAnimation(){
        inFlight = true;
    }

    public void setTarget(Transform t){
        target = t;
    }

    public bool getActive(){
        if(deadge == true || damadge == true){
            return false;
        }
        else 
            return true;
    }

}
