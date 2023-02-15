using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetBattleHandler : MonoBehaviour
{
    //public CapitalShip ship1;
    //public CapitalShip ship2;
    Fleet fleet1;
    public Fleet fleet2;
    // Start is called before the first frame update
    void Start()
    {
        fleet1 = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        //fleet one doesnt have game object, it has component. Use playerdata fleet for loop and set instanciate to fleet 1
        Fleet fleetTemp = GameObject.Find("PlayerData").GetComponent<Fleet>();
        for(int i = 0; i < fleetTemp.CapitalShips.Count; i++){
            Vector3 CapitalPosition = transform.position;
            CapitalPosition.y = CapitalPosition.y + i;
            GameObject CapitalTemp= Instantiate(fleetTemp.CapitalShips[i], CapitalPosition,transform.rotation);
            fleet1.CapitalShips.Add(CapitalTemp);
            //fleet1.CapitalShips[i].setup();
        }
        for(int i = 0; i < fleetTemp.starFighters.Count; i++){
            Vector3 fighterPosition = transform.position;
            fighterPosition.y = fighterPosition.y + i;
            fighterPosition.x = fighterPosition.x+1;
            GameObject FighterTemp = Instantiate(fleetTemp.starFighters[i], fighterPosition,transform.rotation);
            fleet1.starFighters.Add(FighterTemp);
            //fleet1.starFighters[i].setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fleetBattleCalcTest(){
        //capital ships battle
        for(int i = 0; i < fleet2.CapitalShips.Count; i++){
            int x = Random.Range(0,fleet1.CapitalShips.Count);
            Debug.Log("Enemy ship number " + i + " fires at allied ship number " + x);
            fleet1.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet2.CapitalShips[i].GetComponent<CapitalShip>().artilleryPower);
        }
        for(int i = 0; i < fleet1.CapitalShips.Count; i++){
            int x = Random.Range(0,fleet2.CapitalShips.Count);
            Debug.Log("Allied ship number " + i + " fires at enemy ship number " + x);
            fleet2.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet1.CapitalShips[i].GetComponent<CapitalShip>().artilleryPower);
        }
        //starfighter battle
        List<GameObject> fleet1ActiveFighters = fleet1.ActiveFighters();
        List<GameObject> fleet2ActiveFighters = fleet2.ActiveFighters();
        int DogFights = fleet1ActiveFighters.Count;
        if(fleet2ActiveFighters.Count < DogFights){
            DogFights = fleet2ActiveFighters.Count;
        }
        //Debug.Log("Active DogFights " + DogFights);
        //Debug.Log("Fleet 1 active Fighters " + fleet1ActiveFighters.Count);
        //Debug.Log("Fleet 2 active Fighters " + fleet2ActiveFighters.Count);

        if(fleet1ActiveFighters.Count > 0 && fleet2ActiveFighters.Count > 0){
            for(int i = 0; i < DogFights; i++){
                //Debug.Log(i);
                fleet1ActiveFighters[i].GetComponent<StarFighter>().takeFire(fleet2ActiveFighters[i].GetComponent<StarFighter>().Accuracy);
                fleet2ActiveFighters[i].GetComponent<StarFighter>().takeFire(fleet1ActiveFighters[i].GetComponent<StarFighter>().Accuracy);
            }
        }
        //Starfighters bombing capital ships
        if(fleet1ActiveFighters.Count > fleet2ActiveFighters.Count){
            for(;DogFights < fleet1ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet2.CapitalShips.Count);
                fleet2.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet1ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
            }
        }
        else if(fleet2ActiveFighters.Count > fleet1ActiveFighters.Count){
            for(;DogFights < fleet2ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet1.CapitalShips.Count);
                fleet1.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet2ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
            }
        }
        //make some way for capital ships to destory fighters
        //check for victory or defeat
        if(fleet1.ActiveCapitalShips().Count <= 0 && fleet1.ActiveFighters().Count <=0){
            Debug.Log("Lose condition");
        }
        if(fleet2.ActiveCapitalShips().Count <= 0 && fleet2.ActiveFighters().Count <=0){
            Debug.Log("Win condition");
        }
        
    }

    public void StarFighterMiniGame(int score){

    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
