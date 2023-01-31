using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{

    public List<CapitalShip> CapitalShips;
    public List<StarFighter> starFighters;
    //public CapitalShip capitalShip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<StarFighter> ActiveFighters(){
        List<StarFighter> S = starFighters;
        for(int i = 0; i < S.Count; i++){
            if(S[i].getActive() == false){
                S.RemoveAt(i);
            }
        }
        return S;
    }
    
    
}
