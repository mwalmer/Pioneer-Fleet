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
    public bool inCombat;
    Vector3 directionVector;
    public Transform target;
    Vector3 t = new Vector3();
    SpriteRenderer sprite;
    Color color;
    public GameObject projectile;
    float mid = 0.5f;
    StarFighter opponent;
    CapitalShip opponentCapitalShip;
    bool circling;

    bool chasing =false;

    bool chased = false;

    bool arrived = false;

    bool bombingRun = false;

    bool firing = false;
    Vector3 randPos;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        mid = 0.5f;
        randPos = new Vector3(Random.Range(-7f,6f),Random.Range(-3.5f,3.5f));
    }

    // Update is called once per frame
    // rotation continues from circling idk why
    void Update()
    {
        if(inCombat == true){
            if(circling == true)
                circle();
            if(chasing == true){
                transform.position = Vector3.MoveTowards(transform.position, target.position, 1.2f * Time.deltaTime);
                Vector3.RotateTowards(transform.position,target.position, 1f * Time.deltaTime,1.0f);
            }
            if(chased == true){
                transform.position = Vector3.MoveTowards(transform.position, randPos, 1.4f * Time.deltaTime);
                if(Vector3.Distance(randPos,transform.position) < 0.5f){
                     randPos = new Vector3(Random.Range(-7f,6f),Random.Range(-3.5f,3.5f));
                }
            }
            if(bombingRun == true){
                transform.position = Vector3.MoveTowards(transform.position, target.position, 1.2f * Time.deltaTime);
            }
            
        }
    }

    public void startCombat(bool dogFight){
        inCombat = true;
        firing = true;
        StartCoroutine(Fire());
        if(dogFight == true){
            circling = true;
            StartCoroutine(Behavior());
        }
        else{
            bombingRun = true;
        }
    }

    public void endCombat(){
        firing = false;
        inCombat = false;
    }

    public IEnumerator Fire(){
        while(firing){
            Instantiate(projectile, this.transform.position, this.transform.rotation);
            yield return new WaitForSeconds( Random.Range(1f,2f));
        }   
    }

    public IEnumerator Behavior(){

        yield return new WaitForSeconds(Random.Range(5f,25f));
        circling = false;
        if(chasing == false && chased == false && inCombat == true){
            chasing = true;
            opponent.chased = true;
            opponent.circling = false;
        }
    }

    public void circle(){
        //(Mathf.Cos(mid)*Mathf.Cos(2*mid)
        float x = Random.Range(-0.1f,0.2f);
        float step =  x * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        Vector3 midpoint = Vector3.Lerp(transform.position, target.position, mid);
        transform.RotateAround(midpoint, Vector3.forward, 45 * Time.deltaTime);
    }

    public void chase(){

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


    public void setTarget(StarFighter t){
        target = t.transform;
        opponent = t;
    }

    public void setTargetBombing(CapitalShip t){
        target = t.transform;
        opponentCapitalShip = t;
    }

    public bool getActive(){
        if(deadge == true || damadge == true){
            return false;
        }
        else 
            return true;
    }

}
