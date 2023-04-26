using System.Collections;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] subweapons;
    public float overLoadDuration = 5f;
    public GameObject playerPart1;
    public GameObject playerPart2;
    public StarfighterOverloadSystem overloadSys;

    public UI_WeaponSwitcher mainWeaponSwitcher;
    public UI_WeaponSwitcher subWeaponSwitcher;

    private int activeWeaponIndex = 0;
    private int activeSubweaponIndex = 0;
    private bool isOverLoad = false;


    private void Start()
    {
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        foreach (GameObject subweapon in subweapons)
        {
            subweapon.SetActive(false);
        }   //Deactivate all weapons and subweapons at start

        playerPart1.SetActive(false);
        playerPart2.SetActive(false);
        overloadSys.OverloadOff();

        weapons[0].SetActive(true);
        subweapons[0].SetActive(true);//Activate the first weapon and subweapon
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //Deactivate the currently active weapon and activate the next one
            weapons[activeWeaponIndex].SetActive(false);
            activeWeaponIndex = (activeWeaponIndex + 1) % weapons.Length;
            weapons[activeWeaponIndex].SetActive(true);

            if (mainWeaponSwitcher) mainWeaponSwitcher.NextIndicator();

        } //Toggle through main weapons with Q

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Deactivate the currently active subweapon and activate the next one
            subweapons[activeSubweaponIndex].SetActive(false);
            activeSubweaponIndex = (activeSubweaponIndex + 1) % subweapons.Length;
            subweapons[activeSubweaponIndex].SetActive(true);

            if (subWeaponSwitcher) subWeaponSwitcher.NextIndicator();
        } //Toggle through subweapons with E

        if (Input.GetKeyDown(KeyCode.F) && !isOverLoad)
        {
            StartCoroutine(ActivateAllWeapons());
        }
        //Activate all weapons for a short period of time with F
    }

    private IEnumerator ActivateAllWeapons()
    {
        int previousWeaponIndex = activeWeaponIndex;
        int previousSubweaponIndex = activeSubweaponIndex;
        //Remember the previous active weapon and subweapon
        //activate all weapons and subweapons
        playerPart1.SetActive(true);
        playerPart2.SetActive(true);//activate player parts
        overloadSys.OverloadOn();

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(true);
        }

        foreach (GameObject subweapon in subweapons)
        {
            subweapon.SetActive(true);
        }

        //Set isOverLoad flag to true and wait for overLoadDuration seconds
        isOverLoad = true;
        yield return new WaitForSeconds(overLoadDuration);


        for (int i = 0; i < weapons.Length; i++)
        {
            if (i != previousWeaponIndex)
            {
                weapons[i].SetActive(false);
            } //Deactivate all weapons and subweapons except the previous active ones
        }

        for (int i = 0; i < subweapons.Length; i++)
        {
            if (i != previousSubweaponIndex)
            {
                subweapons[i].SetActive(false);
            }
        }
        playerPart1.SetActive(false);
        playerPart2.SetActive(false);//deactivate player parts
        overloadSys.OverloadOff();


        weapons[previousWeaponIndex].SetActive(true);
        subweapons[previousSubweaponIndex].SetActive(true);
        //get back to previous weapons
        isOverLoad = false;

    }
}