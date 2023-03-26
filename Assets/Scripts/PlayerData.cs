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
        setEnemyFleet();
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

    public void setEnemyFleet(){
        CapitalShip enemyCapitalShip = Resources.Load("FleetBattle/NairanBattlecruiser", typeof(CapitalShip)) as CapitalShip;
        //CapitalShip c = test.GetComponent(typeof(GameObject)) as GameObject;
        enemyFleet.CapitalShips.Add(enemyCapitalShip);
        enemyFleet.CapitalShips.Add(enemyCapitalShip);

        StarFighter enemyFigther = Resources.Load("FleetBattle/NairanFighter", typeof(StarFighter)) as StarFighter;
        //StarFighter s = test2.GetComponent(typeof(GameObject)) as GameObject;
        enemyFleet.StarFighters.Add(enemyFigther);
        enemyFleet.StarFighters.Add(enemyFigther);
        enemyFleet.StarFighters.Add(enemyFigther);
        //playerFleet.addCapitalShip(test.GetComponent<CapitalShip>());
        //playerFleet.starFighters.Add((StarFighter)Resources.Load("PlayerFighter", typeof(GameObject)));

    }
}
