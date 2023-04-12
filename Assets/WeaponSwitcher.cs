using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;
    public GameObject[] subweapons;

    private int activeWeaponIndex = 0;
    private int activeSubweaponIndex = 0;

    private void Start()
    {
        // Deactivate all weapons and subweapons at start
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        foreach (GameObject subweapon in subweapons)
        {
            subweapon.SetActive(false);
        }

        // Activate the first weapon and subweapon
        weapons[0].SetActive(true);
        subweapons[0].SetActive(true);
    }

    private void Update()
    {
        // Toggle through main weapons with Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Deactivate the currently active weapon and activate the next one
            weapons[activeWeaponIndex].SetActive(false);
            activeWeaponIndex = (activeWeaponIndex + 1) % weapons.Length;
            weapons[activeWeaponIndex].SetActive(true);
        }

        // Toggle through subweapons with E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Deactivate the currently active subweapon and activate the next one
            subweapons[activeSubweaponIndex].SetActive(false);
            activeSubweaponIndex = (activeSubweaponIndex + 1) % subweapons.Length;
            subweapons[activeSubweaponIndex].SetActive(true);
        }
    }
}