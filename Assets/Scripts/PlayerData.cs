using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Fleet playerFleet;
    public Fleet enemyFleet;
    public int miniGame;
    public int miniGameScore;

    public int currency;

    public bool LoadBridgeFirstTime;

    void Start()
    {
        DontDestroyOnLoad (transform.gameObject);
        playerFleet = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        enemyFleet = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        setFleet();
        //setEnemyFleet("NairanBattlecruiser",2,"NairanFighter",3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFleet(){
        addPlayerCaptialShip("PlayerFrigate");
        addPlayerCaptialShip("PlayerFrigate");
        
        addPlayerStarFighter("PlayerFighter");
        addPlayerStarFighter("PlayerFighter");
        addPlayerStarFighter("PlayerFighter");
    }

    public void offScreen(){
        for(int i = 0; i < playerFleet.CapitalShips.Count;i++){
            playerFleet.CapitalShips[i].transform.position = transform.position;
        }
        for(int i = 0; i < playerFleet.StarFighters.Count;i++){
            playerFleet.StarFighters[i].transform.position = transform.position;
        }

        for(int i = 0; i < enemyFleet.CapitalShips.Count;i++){
            enemyFleet.CapitalShips[i].transform.position = transform.position;
        }
        for(int i = 0; i < enemyFleet.StarFighters.Count;i++){
            enemyFleet.StarFighters[i].transform.position = transform.position;
        }
    }

    public void addPlayerCaptialShip(string name){
        CapitalShip playerCapitalShip = Instantiate(Resources.Load("FleetBattle/" + name, typeof(CapitalShip)) as CapitalShip, transform);
        playerFleet.CapitalShips.Add(playerCapitalShip);
    }

    public void addPlayerStarFighter(string name){
        StarFighter playerStarFighter = Instantiate(Resources.Load("FleetBattle/" + name, typeof(StarFighter)) as StarFighter, transform);
        playerFleet.StarFighters.Add(playerStarFighter);
    }

    public void setEnemyFleet(string captialShipType, int captitalShipNum, string starFighterType, int starFighterNum){
        for(int i = 0; i < captitalShipNum; i++){
            CapitalShip enemyCapitalShip = Instantiate(Resources.Load("FleetBattle/" + captialShipType, typeof(CapitalShip)) as CapitalShip, transform);
            enemyFleet.CapitalShips.Add(enemyCapitalShip);
        }
        for(int i = 0; i < starFighterNum; i++){
            StarFighter enemyFigther = Instantiate(Resources.Load("FleetBattle/" + starFighterType, typeof(StarFighter)) as StarFighter, transform);
            enemyFleet.StarFighters.Add(enemyFigther);
        }
    }

    public void clearEnemyFleet(){
        enemyFleet.CapitalShips.Clear();
        enemyFleet.StarFighters.Clear();
    }

    public void updateHP(int value)
    {
        // might need to have a ship in params to deal the damage to? or randomly choose a ship
        for(int i = 0; i < playerFleet.CapitalShips.Count;i++){
            playerFleet.CapitalShips[i].AddHP(value);
        }
    }

    public void updateCurrency(int value)
    {
        // idk where currency is stored
        currency += value;
    }
}
