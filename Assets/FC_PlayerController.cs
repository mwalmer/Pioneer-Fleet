using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_PlayerController : MonoBehaviour
{
    [Header("Controllers")]
    public KeyCode fireButton = KeyCode.Mouse0;
    public KeyCode reloadButton = KeyCode.R;
    public KeyCode shieldButton = KeyCode.Mouse1;


    [Header("Components")]
    public FC_Cannon cannons;
    public FC_EnergyShield shield;
    public FC_AimmingCursor playerCursor;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayerCommand();
    }

    void DetectPlayerCommand()
    {
        if (Input.GetKey(fireButton))
        {
            cannons.FireCannons();
        }
        if (Input.GetKey(reloadButton))
        {
            cannons.ReloadCannons();
        }
    }

}
