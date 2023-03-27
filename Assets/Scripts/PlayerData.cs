using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Fleet playerFleet;
    public Fleet enemyFleet;
    public int miniGame;
    public int miniGameScore;
    //public List<CapitalShip> CapitalShips;
    // Start is called before the first frame update
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
        CapitalShip enemyCapitalShip = Resources.Load("FleetBattle/" + captialShipType, typeof(CapitalShip)) as CapitalShip;
        for(int i = 0; i < captitalShipNum; i++){
            enemyFleet.CapitalShips.Add(enemyCapitalShip);
        }

        StarFighter enemyFigther = Resources.Load("FleetBattle/" + starFighterType, typeof(StarFighter)) as StarFighter;
        for(int i = 0; i < starFighterNum; i++){
            enemyFleet.StarFighters.Add(enemyFigther);
        }
 

    }
}
