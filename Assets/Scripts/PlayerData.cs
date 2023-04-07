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
    public string FleetLog;

    void Start()
    {
        DontDestroyOnLoad (transform.gameObject);
        playerFleet = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        enemyFleet = gameObject.AddComponent(typeof(Fleet)) as Fleet;
        setFleet();
        FleetLog = "An enemy fleet approaches";
        //setEnemyFleet("NairanBattlecruiser",2,"NairanFighter",3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addToFleetLog(string s){
        FleetLog = FleetLog + s;
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
            playerFleet.CapitalShips[i].gameObject.SetActive(false);
            //playerFleet.CapitalShips[i].transform.position = transform.position;
            //playerFleet.CapitalShips[i].transform.rotation = transform.rotation;
        }
        for(int i = 0; i < playerFleet.StarFighters.Count;i++){
            playerFleet.StarFighters[i].gameObject.SetActive(false);
            playerFleet.StarFighters[i].endCombat();
            //playerFleet.StarFighters[i].transform.position = transform.position;
            //playerFleet.StarFighters[i].transform.rotation = transform.rotation;

        }

        for(int i = 0; i < enemyFleet.CapitalShips.Count;i++){
            enemyFleet.CapitalShips[i].gameObject.SetActive(false);
            //enemyFleet.CapitalShips[i].transform.position = transform.position;
            //enemyFleet.CapitalShips[i].transform.rotation = transform.rotation;

        }
        for(int i = 0; i < enemyFleet.StarFighters.Count;i++){
            enemyFleet.StarFighters[i].gameObject.SetActive(false);
            enemyFleet.StarFighters[i].endCombat();
            //enemyFleet.StarFighters[i].transform.position = transform.position;
            //enemyFleet.StarFighters[i].transform.rotation = transform.rotation;

        }
    }

    public void HandleDamageandDeadFighters(){
        for(int i = 0; i < playerFleet.StarFighters.Count;i++){
            if(playerFleet.StarFighters[i].damadge == true)
                playerFleet.StarFighters[i].damadge = false;
            if(playerFleet.StarFighters[i].deadge == true)
                playerFleet.StarFighters.RemoveAt(i);
        }
    }

      public List<StarFighter> PlayerActiveFighters(){
        List<StarFighter> S = new List<StarFighter>();
        for(int i = 0; i < playerFleet.StarFighters.Count; i++){
            if(playerFleet.StarFighters[i].getActive() == true){
                S.Add(playerFleet.StarFighters[i]);
            }
        }
        return S;
    }

    
      public List<StarFighter> EnemyActiveFighters(){
        List<StarFighter> S = new List<StarFighter>();
        for(int i = 0; i < enemyFleet.StarFighters.Count; i++){
            if(enemyFleet.StarFighters[i].getActive() == true){
                S.Add(enemyFleet.StarFighters[i]);
            }
        }
        return S;
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
        for(int i = 0; i < enemyFleet.CapitalShips.Count;i++){
            Destroy(enemyFleet.CapitalShips[i].gameObject);
            //enemyFleet.CapitalShips[i].transform.position = transform.position;
            //enemyFleet.CapitalShips[i].transform.rotation = transform.rotation;

        }
        for(int i = 0; i < enemyFleet.StarFighters.Count;i++){
            Destroy(enemyFleet.StarFighters[i].gameObject);

        }
        //enemyFleet.CapitalShips.Clear();
        //enemyFleet.StarFighters.Clear();
    }

    public void updateHP(int value)
    {
        for(int i = 0; i < playerFleet.CapitalShips.Count;i++){
            playerFleet.CapitalShips[i].AddHP(value);
        }
    }

    public void updateCurrency(int value)
    {
        currency += value;
    }
}
