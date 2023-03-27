using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FleetBattleHandler : MonoBehaviour
{
    //public CapitalShip ship1;
    //public CapitalShip ship2;
    Fleet fleet1;
    Fleet fleet2; 
    CapitalShip CapitalTemp;
    
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        Fleet[] fleetTemp = GameObject.Find("PlayerData").GetComponents<Fleet>();
        fleet1 = fleetTemp[0];
        for(int i = 0; i < fleet1.CapitalShips.Count; i++){
            Vector3 CapitalPosition = transform.position;
            CapitalPosition.y = CapitalPosition.y + i;
            fleet1.CapitalShips[i].gameObject.transform.position = CapitalPosition;
        }
        for(int i = 0; i < fleet1.StarFighters.Count; i++){
            Vector3 fighterPosition = transform.position;
            fighterPosition.y = fighterPosition.y + i;
            fighterPosition.x = fighterPosition.x+1;
            fleet1.StarFighters[i].gameObject.transform.position = fighterPosition;
        }

    fleet2 = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        //fleet one doesnt have game object, it has component. Use playerdata fleet for loop and set instanciate to fleet 1
        //Fleet fleetTemp2 = GameObject.Find("PlayerData").GetComponent<Fleet>();
        for(int i = 0; i < fleetTemp[1].CapitalShips.Count; i++){
            Vector3 CapitalPosition = transform.position;
            CapitalPosition.y = CapitalPosition.y + i;
            CapitalPosition.x = CapitalPosition.x+5;
            CapitalTemp= Instantiate(fleetTemp[1].CapitalShips[i], CapitalPosition,transform.rotation*Quaternion.Euler(180f, 180f, 0));
            fleet2.CapitalShips.Add(CapitalTemp.GetComponent<CapitalShip>());
            //fleet1.CapitalShips[i].setup();
        }
        for(int i = 0; i < fleetTemp[1].StarFighters.Count; i++){
            Vector3 fighterPosition = transform.position;
            fighterPosition.y = fighterPosition.y + i;
            fighterPosition.x = fighterPosition.x+4;
            StarFighter FighterTemp = Instantiate(fleetTemp[1].StarFighters[i], fighterPosition,transform.rotation*Quaternion.Euler(180f, 180f, 0));
            fleet2.StarFighters.Add(FighterTemp);
            //fleet1.starFighters[i].setup();
        }
        if(GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame != 0){
            Debug.Log("You just played a mini game. Good job");
            MiniGameResults(GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame,GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGameScore);
        }

        setHPtext();
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
        List<StarFighter> fleet1ActiveFighters = fleet1.ActiveFighters();
        List<StarFighter> fleet2ActiveFighters = fleet2.ActiveFighters();
        int DogFights = fleet1ActiveFighters.Count;
        if(fleet2ActiveFighters.Count < DogFights){
            DogFights = fleet2ActiveFighters.Count;
        }
        //Debug.Log("Active DogFights " + DogFights);
        //Debug.Log("Fleet 1 active Fighters " + fleet1ActiveFighters.Count);
        //Debug.Log("Fleet 2 active Fighters " + fleet2ActiveFighters.Count);

        if(fleet1ActiveFighters.Count > 0 && fleet2ActiveFighters.Count > 0){
            for(int i = 0; i < DogFights; i++){
                //fleet1ActiveFighters[i].GetComponent<StarFighter>().setTarget(fleet2ActiveFighters[i].transform);
                //fleet2ActiveFighters[i].GetComponent<StarFighter>().setTarget(fleet1ActiveFighters[i].transform);

                //fleet1ActiveFighters[i].GetComponent<StarFighter>().dogFightAnimation();
                //fleet2ActiveFighters[i].GetComponent<StarFighter>().dogFightAnimation();

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
        setHPtext();
        //make some way for capital ships to destory fighters
        //check for victory or defeat
        if (fleet1.ActiveCapitalShips().Count <= 0 && fleet1.ActiveFighters().Count <=0){
            Debug.Log("Lose condition");
        }
        if(fleet2.ActiveCapitalShips().Count <= 0 && fleet2.ActiveFighters().Count <=0){
            Debug.Log("Win condition");
        }
        Debug.Log("There are this many allied capital ships this number should never change from 2 and it is " + fleet1.CapitalShips.Count);
        Debug.Log("From the feel scene directly Ship #0 has Hull:" + fleet1.CapitalShips[0].currentHull + " and Shield:" + fleet1.CapitalShips[0].currentShield);
        Debug.Log("From the feel scene directly Ship #1 has Hull:" + fleet1.CapitalShips[1].currentHull + " and Shield:" + fleet1.CapitalShips[1].currentShield);

        //Fleet fleetTestOG = GameObject.Find("PlayerData").GetComponent<Fleet>() as Fleet;
        //fleetTestOG.setHP(fleet1);
        GameObject.Find("PlayerData").GetComponent<Fleet>().setHP(fleet1);
        //Fleet fleetTest = GameObject.Find("PlayerData").GetComponent<Fleet>() as Fleet;
        Debug.Log("Ship #0 has Hull:" + GameObject.Find("PlayerData").GetComponent<Fleet>().CapitalShips[0].currentHull + " and Shield:" + GameObject.Find("PlayerData").GetComponent<Fleet>().CapitalShips[0].currentShield);
        Debug.Log("Ship #1 has Hull:" + GameObject.Find("PlayerData").GetComponent<Fleet>().CapitalShips[1].currentHull + " and Shield:" + GameObject.Find("PlayerData").GetComponent<Fleet>().CapitalShips[1].currentShield);


    }

    //IEnumerator starFighterBattle(){
    //    while()
    //}
    public void setHPtext() {
        text.text = "Ship 1 current hull: " + fleet1.CapitalShips[0].currentHull + "\nShip 1 current shield: " + fleet1.CapitalShips[0].currentShield + "\nShip 2 current hull:" + fleet1.CapitalShips[1].currentHull + "\nShip 2 current shield: " + fleet1.CapitalShips[1].currentShield;
    }

    public void MiniGameResults(int score,int minigame){
        if(minigame == 0){
            Debug.Log("No mini game found");
        }
        switch(minigame){
            case 1: //star fighter
                List<StarFighter> fleet2ActiveFighters = fleet2.ActiveFighters();
                fleet2ActiveFighters[0].GetComponent<StarFighter>().takeFire(score);
                break;
        }
    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
