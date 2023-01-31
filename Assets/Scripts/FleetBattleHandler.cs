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
        fleet1.CapitalShips[0].takeDamage(fleet2.CapitalShips[0].artilleryPower);
        fleet1.CapitalShips[1].takeDamage(fleet2.CapitalShips[1].artilleryPower);
        fleet1.CapitalShips[2].takeDamage(fleet2.CapitalShips[2].artilleryPower);

        fleet2.CapitalShips[0].takeDamage(fleet1.CapitalShips[0].artilleryPower);
        fleet2.CapitalShips[1].takeDamage(fleet1.CapitalShips[1].artilleryPower);
        fleet2.CapitalShips[2].takeDamage(fleet1.CapitalShips[2].artilleryPower);
    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
