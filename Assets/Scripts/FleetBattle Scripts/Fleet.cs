using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{

    public List<GameObject> CapitalShips = new List<GameObject>();
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
/*
    public void addCapitalShip(CapitalShip test){
        CapitalShips.Add(test);
    }
    */
    
}
