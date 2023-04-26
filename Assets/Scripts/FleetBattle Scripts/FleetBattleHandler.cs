using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class FleetBattleHandler : MonoBehaviour
{
    //public CapitalShip ship1;
    //public CapitalShip ship2;
    Fleet fleet1;
    Fleet fleet2; 
    CapitalShip CapitalTemp;
    List<StarFighter> fleet1ActiveFighters;
    List<StarFighter> fleet2ActiveFighters;
    List<CapitalShip> fleet1ActiveCaptialShips;
    List<CapitalShip> fleet2ActiveCaptialShips;
    int DogFights;
    public TMP_Text text;

    public TMP_Text log;

    public Slider PlayerValue;

    public Slider EnemyValue;

    public TMP_Text turnCounter;

    public PlayerData playerData;

    // Start is called before the first frame update
    void Start()
    {
        playerData = GameObject.Find("PlayerData").GetComponent<PlayerData>();
        bool firstTime = playerData.LoadBridgeFirstTime;
        Fleet[] fleetTemp = GameObject.Find("PlayerData").GetComponents<Fleet>();
        fleet1 = fleetTemp[0];
        fleet2 = fleetTemp[1];
        if(firstTime == true){
            playerData.clearFleetLog();
            PlayerValue.GetComponent<UI_ProgressBar>().value = 1f;
            EnemyValue.GetComponent<UI_ProgressBar>().value = 1f;
        }

        turnCounter.text = "Turn " + playerData.battleTurn;
        playerData.addToFleetLog("\n-------Turn "+ playerData.battleTurn+"--------\n");
        playerData.battleTurn += 1;
        setHPtext();
        if(firstTime == false){
            if(fleetBattleCalcTest(1,playerData.miniGameScore))
                return;
            setHPtext();
        }
        playerData.LoadBridgeFirstTime = false;
        log.text = playerData.FleetLog;
        fleet1ActiveFighters = playerData.PlayerActiveFighters();
        fleet2ActiveFighters = playerData.EnemyActiveFighters();
        fleet1ActiveCaptialShips = playerData.PlayerActiveCapitalShips();
        fleet2ActiveCaptialShips = playerData.EnemyActiveCapitalShips();
        DogFights = fleet1ActiveFighters.Count;
        if(fleet2ActiveFighters.Count < DogFights){
            DogFights = fleet2ActiveFighters.Count;
        }

        playerData.addToFleetLog("Enemy Capital Ships: " + fleet2ActiveCaptialShips.Count + "\nEnemy Star Fighters: " +fleet2ActiveFighters.Count + "\nAllied Capital Ships: " + fleet1ActiveCaptialShips.Count + "\nAllied Star Fighters: " +fleet1ActiveFighters.Count);
        log.text = playerData.FleetLog;
        for(int i = 0; i < fleet1ActiveCaptialShips.Count; i++){
            fleet1ActiveCaptialShips[i].gameObject.SetActive(true);
            Vector3 CapitalPosition = transform.position;
            Quaternion CapitalRotation = transform.rotation;
            CapitalPosition.y = CapitalPosition.y + i;
            fleet1ActiveCaptialShips[i].startCombat();
            fleet1ActiveCaptialShips[i].gameObject.transform.position = CapitalPosition;
            fleet1ActiveCaptialShips[i].gameObject.transform.rotation = CapitalRotation;
        }
        for(int i = 0; i < fleet1ActiveFighters.Count; i++){
            fleet1ActiveFighters[i].gameObject.SetActive(true);
            Vector3 fighterPosition = transform.position;
            Quaternion FighterRotation = transform.rotation;
            fighterPosition.y = fighterPosition.y + i;
            fighterPosition.x = fighterPosition.x+1;
            fleet1ActiveFighters[i].gameObject.transform.position = fighterPosition;
            fleet1ActiveFighters[i].gameObject.transform.rotation = FighterRotation;

        }

        for(int i = 0; i < fleet2ActiveCaptialShips.Count; i++){
            fleet2ActiveCaptialShips[i].gameObject.SetActive(true);
            Vector3 CapitalPosition = transform.position;
            CapitalPosition.y = CapitalPosition.y + i;
            CapitalPosition.x = CapitalPosition.x+5;
            fleet2ActiveCaptialShips[i].startCombat();
            fleet2ActiveCaptialShips[i].gameObject.transform.position = CapitalPosition;
            Quaternion CapitalRotation =  Quaternion.Inverse(transform.rotation);
            fleet2ActiveCaptialShips[i].gameObject.transform.rotation = CapitalRotation;

            //if(firstTime == true)
            //    fleet2.CapitalShips[i].gameObject.transform.Rotate(Vector3.back*180);
        }
        for(int i = 0; i < fleet2ActiveFighters.Count; i++){
            fleet2ActiveFighters[i].gameObject.SetActive(true);
            Vector3 fighterPosition = transform.position;
            fighterPosition.y = fighterPosition.y + i;
            fighterPosition.x = fighterPosition.x+4;
            fleet2ActiveFighters[i].gameObject.transform.position = fighterPosition;
            Quaternion FighterRotation =  Quaternion.Inverse(transform.rotation);
            fleet2ActiveFighters[i].gameObject.transform.rotation = FighterRotation;
        }

        //Star figther animations
        if(fleet1ActiveFighters.Count > 0 && fleet2ActiveFighters.Count > 0){
            for(int i = 0; i < DogFights; i++){
                fleet1ActiveFighters[i].GetComponent<StarFighter>().startCombat(true);
                fleet2ActiveFighters[i].GetComponent<StarFighter>().startCombat(true);
                fleet1ActiveFighters[i].GetComponent<StarFighter>().setTarget(fleet2ActiveFighters[i]);
                fleet2ActiveFighters[i].GetComponent<StarFighter>().setTarget(fleet1ActiveFighters[i]);

            }
        }
        
        //Starfighters bombing capital ships
        if(fleet1ActiveFighters.Count > fleet2ActiveFighters.Count){
            for(;DogFights < fleet1ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet2ActiveCaptialShips.Count);
                //fleet2.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet1ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
                fleet1ActiveFighters[DogFights].GetComponent<StarFighter>().startCombat(false);
                if(fleet2ActiveCaptialShips.Count != 0)
                    fleet1ActiveFighters[DogFights].GetComponent<StarFighter>().setTargetBombing(fleet2ActiveCaptialShips[x]);
            }
        }
        else if(fleet2ActiveFighters.Count > fleet1ActiveFighters.Count){
            for(;DogFights < fleet2ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet1ActiveCaptialShips.Count);
                fleet2ActiveFighters[DogFights].GetComponent<StarFighter>().startCombat(false);
                if(fleet2ActiveCaptialShips.Count != 0)
                    fleet2ActiveFighters[DogFights].GetComponent<StarFighter>().setTargetBombing(fleet1ActiveCaptialShips[x]);
                //fleet1.CapitalShips[x].GetComponent<CapitalShip>().takeDamage(fleet2ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
            }
        }
        Debug.Log("When the scene ends, we should not make it here. After the last battle. If we do something is fucked");

        //setHPtext();

    }

    public bool fleetBattleCalcTest(int miniGame, int miniGameScore){
        //capital ships battle
        fleet1ActiveCaptialShips = playerData.PlayerActiveCapitalShips();
        fleet2ActiveCaptialShips = playerData.EnemyActiveCapitalShips();
        for(int i = 0; i < fleet2.ActiveCapitalShips().Count; i++){
            int x = Random.Range(0,fleet1ActiveCaptialShips.Count);
            Debug.Log("Enemy ship number " + i + " fires at allied ship number " + x);
            fleet1ActiveCaptialShips[x].GetComponent<CapitalShip>().takeDamage(fleet2ActiveCaptialShips[i].GetComponent<CapitalShip>().artilleryPower);
        }
        for(int i = 0; i < fleet1.ActiveCapitalShips().Count; i++){
            int x = Random.Range(0,fleet2ActiveCaptialShips.Count);
            Debug.Log("Allied ship number " + i + " fires at enemy ship number " + x);
            fleet2ActiveCaptialShips[x].GetComponent<CapitalShip>().takeDamage(fleet1ActiveCaptialShips[i].GetComponent<CapitalShip>().artilleryPower);
        }
        //starfighter battle
        fleet1ActiveFighters = playerData.PlayerActiveFighters();
        fleet2ActiveFighters = playerData.EnemyActiveFighters();
        DogFights = fleet1ActiveFighters.Count;
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
                fleet2ActiveFighters[i].GetComponent<StarFighter>().takeFire(fleet1ActiveFighters[i].GetComponent<StarFighter>().Accuracy);
                fleet1ActiveFighters[i].GetComponent<StarFighter>().takeFire(fleet2ActiveFighters[i].GetComponent<StarFighter>().Accuracy);

            }
        }
        //Starfighters bombing capital ships
        if(fleet1ActiveFighters.Count > fleet2ActiveFighters.Count){
            for(;DogFights < fleet1ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet2ActiveCaptialShips.Count);
                fleet2ActiveCaptialShips[x].GetComponent<CapitalShip>().takeDamage(fleet1ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
            }
        }
        else if(fleet2ActiveFighters.Count > fleet1ActiveFighters.Count){
            for(;DogFights < fleet2ActiveFighters.Count; DogFights++){
                int x = Random.Range(0,fleet1ActiveCaptialShips.Count);
                fleet1ActiveCaptialShips[x].GetComponent<CapitalShip>().takeDamage(fleet2ActiveFighters[DogFights].GetComponent<StarFighter>().BombingPower);
            }
        }
        setHPtext();
        //make some way for capital ships to destory fighters
        //check for victory or defeat
        Debug.Log("There are this many allied capital ships this number should never change from 2 and it is " + fleet1.CapitalShips.Count);
        Debug.Log("From the feel scene directly Ship #0 has Hull:" + fleet1.CapitalShips[0].currentHull + " and Shield:" + fleet1.CapitalShips[0].currentShield);
        Debug.Log("From the feel scene directly Ship #1 has Hull:" + fleet1.CapitalShips[1].currentHull + " and Shield:" + fleet1.CapitalShips[1].currentShield);

        //Fleet fleetTestOG = GameObject.Find("PlayerData").GetComponent<Fleet>() as Fleet;
        //fleetTestOG.setHP(fleet1);
        GameObject.Find("PlayerData").GetComponent<Fleet>().setHP(fleet1);
        //Fleet fleetTest = GameObject.Find("PlayerData").GetComponent<Fleet>() as Fleet;

        if(playerData.miniGame == 0){
            Debug.Log("No mini game found");
            }
        switch(playerData.miniGame){
            case 1: //star fighter
                fleet2ActiveCaptialShips = playerData.EnemyActiveCapitalShips();
                if(fleet2ActiveCaptialShips.Count > 0){
                    fleet2ActiveCaptialShips[0].GetComponent<CapitalShip>().takeDamage(miniGameScore);
                    playerData.addToFleetLog("Your ace pilot did " + miniGameScore + " damage\n");
                }
                break;
            case 2: //flak cannon
                fleet2ActiveFighters = playerData.EnemyActiveFighters();
                if(fleet2ActiveFighters.Count > 0){
                    if(fleet2ActiveFighters[0].GetComponent<StarFighter>().takeFire(miniGameScore)){
                        playerData.addToFleetLog("Your gunner crew destroyed 1 starfighter\n");
                    }
                }
                break;
            case 3: //match 3
                fleet2ActiveCaptialShips = playerData.EnemyActiveCapitalShips();
                if(fleet2ActiveCaptialShips.Count > 0){
                    fleet2ActiveCaptialShips[0].GetComponent<CapitalShip>().takeDamage(miniGameScore);
                    playerData.addToFleetLog("Your munitions did " + miniGameScore + " damage\n");
                }
                break;
        }
        if (playerData.PlayerActiveCapitalShips().Count <= 0){
                Debug.Log("Lose condition");
                Destroy(GameObject.Find("PlayerData"));
                SceneManager.LoadScene(0);
                return false;
            }
        if(playerData.EnemyActiveCapitalShips().Count <= 0){
                playerData.offScreen();
                playerData.clearEnemyFleet();
                playerData.HandleDamageandDeadFighters();
                playerData.battleTurn = 1;
                SceneManager.LoadScene(1);
                return true;
                Debug.Log("Win condition");
            }
        return false;
    }


    //IEnumerator starFighterBattle(){
    //    while()
    //}
    public void setHPtext() {
        PlayerValue.GetComponent<UI_ProgressBar>().value = playerData.PlayerFleetHealth();
        EnemyValue.GetComponent<UI_ProgressBar>().value = playerData.EnemyFleetHealth();
        //text.text = "Ship 1 current hull: " + fleet1.CapitalShips[0].currentHull + "\nShip 1 current shield: " + fleet1.CapitalShips[0].currentShield + "\nShip 2 current hull:" + fleet1.CapitalShips[1].currentHull + "\nShip 2 current shield: " + fleet1.CapitalShips[1].currentShield;
    }

    public void MiniGameResults(int score,int minigame){
        if(minigame == 0){
            Debug.Log("No mini game found");
        }
        switch(minigame){
            case 1: //star fighter
                //List<StarFighter> fleet2ActiveFighters = fleet2.ActiveFighters();
                fleet2ActiveFighters[0].GetComponent<StarFighter>().takeFire(score);
                break;
        }

        
    }

    //public void battle1v1(){
        //ship1.takeDamage(ship2.artilleryPower);
        //ship2.takeDamage(ship1.artilleryPower);
    //}
}
