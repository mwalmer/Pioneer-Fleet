using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Fleet playerFleet;
    public Fleet enemyFleet;
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
        GameObject test = Resources.Load("PlayerFrigate", typeof(GameObject)) as GameObject;
        //CapitalShip c = test.GetComponent(typeof(GameObject)) as GameObject;
        playerFleet.CapitalShips.Add(test);
        playerFleet.CapitalShips.Add(test);

        GameObject test2 = Resources.Load("PlayerFighter", typeof(GameObject)) as GameObject;
        //StarFighter s = test2.GetComponent(typeof(GameObject)) as GameObject;
        playerFleet.starFighters.Add(test2);
        playerFleet.starFighters.Add(test2);
        playerFleet.starFighters.Add(test2);
        //playerFleet.addCapitalShip(test.GetComponent<CapitalShip>());
        //playerFleet.starFighters.Add((StarFighter)Resources.Load("PlayerFighter", typeof(GameObject)));

    }

    public void setEnemyFleet(){
        GameObject test = Resources.Load("PlayerFrigate", typeof(GameObject)) as GameObject;
        //CapitalShip c = test.GetComponent(typeof(GameObject)) as GameObject;
        enemyFleet.CapitalShips.Add(test);
        enemyFleet.CapitalShips.Add(test);

        GameObject test2 = Resources.Load("PlayerFighter", typeof(GameObject)) as GameObject;
        //StarFighter s = test2.GetComponent(typeof(GameObject)) as GameObject;
        enemyFleet.starFighters.Add(test2);
        enemyFleet.starFighters.Add(test2);
        enemyFleet.starFighters.Add(test2);
        //playerFleet.addCapitalShip(test.GetComponent<CapitalShip>());
        //playerFleet.starFighters.Add((StarFighter)Resources.Load("PlayerFighter", typeof(GameObject)));

    }
}
