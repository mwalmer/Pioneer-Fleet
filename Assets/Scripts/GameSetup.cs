using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerState.playerFleet.CapitalShips.Add((CapitalShip)Resources.Load("prefabs/Kla'ed Frigate", typeof(GameObject)));
        PlayerState.playerFleet.CapitalShips.Add((CapitalShip)Resources.Load("prefabs/Kla'ed Frigate", typeof(GameObject)));

        PlayerState.playerFleet.starFighters.Add((StarFighter)Resources.Load("prefabs/Kla'ed Fighter", typeof(GameObject)));
        PlayerState.playerFleet.starFighters.Add((StarFighter)Resources.Load("prefabs/Kla'ed Fighter", typeof(GameObject)));
        PlayerState.playerFleet.starFighters.Add((StarFighter)Resources.Load("prefabs/Kla'ed Fighter", typeof(GameObject)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
