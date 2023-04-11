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
    public FC_ShieldGenerator shield;
    public FC_AimmingCursor playerCursor;

    public static FC_PlayerController MainPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        MainPlayerController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (FC_GameManager.IsGameActive == false) return;

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
        if (Input.GetKey(shieldButton))
        {
            shield.ChargingShield();
        }
        else if (Input.GetKeyUp(shieldButton))
        {
            shield.GenerateShield(playerCursor.GetPos());
        }
    }


}
