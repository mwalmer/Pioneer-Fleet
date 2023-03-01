using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{

    public List<GameObject> CapitalShips = new List<GameObject>();

    public List<int> CapitalShipsCurrentHull = new List<int>();
    public List<int> CapitalShipsCurrentShields = new List<int>();

    public List<GameObject> starFighters = new List<GameObject>();
    //public CapitalShip capitalShip;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<GameObject> ActiveFighters(){
        List<GameObject> S = starFighters;
        for(int i = 0; i < S.Count; i++){
            if(S[i].GetComponent<StarFighter>().getActive() == false){
                S.RemoveAt(i);
            }
        }
        return S;
    }

    public List<GameObject> ActiveCapitalShips(){
        List<GameObject> C = CapitalShips;
        for(int i = 0; i < C.Count; i++){
            if(C[i].GetComponent<CapitalShip>().getActive() == false){
                C.RemoveAt(i);
            }
        }
        return C;
    }

    public void setHP(Fleet f){
        for(int i = 0; i < CapitalShips.Count;i++){
            CapitalShipsCurrentHull[i] = f.CapitalShips[i].GetComponent<CapitalShip>().currentHull;
            CapitalShipsCurrentShields[i] = f.CapitalShips[i].GetComponent<CapitalShip>().currentShield;
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
