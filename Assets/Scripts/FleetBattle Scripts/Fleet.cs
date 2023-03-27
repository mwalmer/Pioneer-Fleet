using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{

    public List<CapitalShip> CapitalShips = new List<CapitalShip>();

    public List<StarFighter> StarFighters = new List<StarFighter>();
    //public CapitalShip capitalShip;
    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update(){
        
    }

    public List<StarFighter> ActiveFighters(){
        List<StarFighter> S = new List<StarFighter>();
        for(int i = 0; i < StarFighters.Count; i++){
            if(StarFighters[i].getActive() == true){
                S.Add(StarFighters[i]);
            }
        }
        return S;
    }


    public List<CapitalShip> ActiveCapitalShips(){
        List<CapitalShip> S = new List<CapitalShip>();
        for(int i = 0; i < CapitalShips.Count; i++){
            if(CapitalShips[i].getActive() == true){
                S.Add(CapitalShips[i]);
            }
        }
        return S;
    }

    public void setHP(Fleet f){
        //CapitalShips = f.CapitalShips;
        for (int i = 0; i < CapitalShips.Count;i++){
            Debug.Log("In setHP Ship #" + i + " has Hull:" + f.CapitalShips[i].currentHull + " and Shield:" + f.CapitalShips[i].currentShield);
            //CapitalShips[i].currentHull = f.CapitalShips[i].currentHull;
            //CapitalShips[i].currentShield = f.CapitalShips[i].currentShield;
            CapitalShips[i].setHP(f.CapitalShips[i].currentHull,f.CapitalShips[i].currentShield);
            Debug.Log("In setHP After we set it Ship #" + i + " has Hull:" + CapitalShips[i].currentHull + " and Shield:" + CapitalShips[i].currentShield);

            //starFighters[i].GetComponent<StarFighter>().damadge = f.starFighters[i].GetComponent<StarFighter>().damadge;
            //starFighters[i].GetComponent<StarFighter>().deadge = f.starFighters[i].GetComponent<StarFighter>().deadge;
        }
    }
/*
    public void addCapitalShip(CapitalShip test){
        CapitalShips.Add(test);
    }
    */
    
}
