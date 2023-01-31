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
        for(int i = 0; i < fleet2.CapitalShips.Length; i++){
            int x = Random.Range(0,fleet2.CapitalShips.Length);
            Debug.Log(x);
            fleet1.CapitalShips[x].takeDamage(fleet2.CapitalShips[i].artilleryPower);
        }
        for(int i = 0; i < fleet1.CapitalShips.Length; i++){
            int x = Random.Range(0,fleet2.CapitalShips.Length);
            Debug.Log(x);
            fleet2.CapitalShips[x].takeDamage(fleet1.CapitalShips[i].artilleryPower);
        }
        int DogFights = fleet1.starFighters.Length;
        if(fleet2.starFighters.Length < DogFights){
            DogFights = fleet2.starFighters.Length;
        }
        for(int i = 0; i < DogFights; i++){
            fleet1.starFighters[i].takeFire(fleet2.starFighters[i].Accuracy);
            fleet2.starFighters[i].takeFire(fleet1.starFighters[i].Accuracy);
        }
    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
