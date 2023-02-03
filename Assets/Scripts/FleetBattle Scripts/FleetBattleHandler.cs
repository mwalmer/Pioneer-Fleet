using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetBattleHandler : MonoBehaviour
{
    //public CapitalShip ship1;
    //public CapitalShip ship2;
    public Fleet fleet1;
    public Fleet fleet2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fleetBattleCalcTest(){
        //capital ships battle
        for(int i = 0; i < fleet2.CapitalShips.Count; i++){
            int x = Random.Range(0,fleet2.CapitalShips.Count);
            fleet1.CapitalShips[x].takeDamage(fleet2.CapitalShips[i].artilleryPower);
        }
        for(int i = 0; i < fleet1.CapitalShips.Count; i++){
            int x = Random.Range(0,fleet2.CapitalShips.Count);
            fleet2.CapitalShips[x].takeDamage(fleet1.CapitalShips[i].artilleryPower);
        }
        //starfighter battle
        List<StarFighter> fleet1ActiveFighters = fleet1.ActiveFighters();
        List<StarFighter> fleet2ActiveFighters = fleet2.ActiveFighters();
        int DogFights = fleet1ActiveFighters.Count;
        if(fleet2ActiveFighters.Count < DogFights){
            DogFights = fleet2ActiveFighters.Count;
        }
        Debug.Log("Active DogFights " + DogFights);
        Debug.Log("Fleet 1 active Fighters " + fleet1ActiveFighters.Count);
        Debug.Log("Fleet 2 active Fighters " + fleet2ActiveFighters.Count);

        if(fleet1ActiveFighters.Count > 0 && fleet2ActiveFighters.Count > 0){
            for(int i = 0; i < DogFights; i++){
                Debug.Log(i);
                fleet1ActiveFighters[i].takeFire(fleet2ActiveFighters[i].Accuracy);
                fleet2ActiveFighters[i].takeFire(fleet1ActiveFighters[i].Accuracy);
            }
        }
        
        if(fleet1ActiveFighters.Count > fleet2ActiveFighters.Count){
            for(;DogFights < fleet1ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet2.CapitalShips.Count);
                fleet2.CapitalShips[x].takeDamage(fleet1ActiveFighters[DogFights].BombingPower);
            }
        }
        else if(fleet2ActiveFighters.Count > fleet1ActiveFighters.Count){
            for(;DogFights < fleet2ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet1.CapitalShips.Count);
                fleet1.CapitalShips[x].takeDamage(fleet2ActiveFighters[DogFights].BombingPower);
            }
        }
        
    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
