using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starFighterPlayerResources : MonoBehaviour
{

    public UI_IconBar ui_hp;
    public UI_IconBar ui_energy;
    public DamageHandler player_hp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ui_hp.ChangeBarValue(player_hp.health);
         ui_energy.ChangeBarValue(player_hp.energy);
    }
}