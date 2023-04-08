using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_Cannon : MonoBehaviour
{
    public Transform leftCannon;
    public Transform rightCannon;
    public float offset = 38f;
    public Transform cursorLocation;


    // Cannon Attributes
    [Header("Cannon Components")]
    Charger leftCannonCharger;
    Charger rightCannonCharger;
    Charger switchCannon;
    public FC_CannonBullet bullet;
    public int ammoMagazine = 36;
    int bulletAmmo = 36;
    public float firePower = 1000;
    public Transform leftMuzzle;
    public Transform rightMuzzle;
    public Transform bulletCollector;
    // Reloading
    public KeyCode reloadButton = KeyCode.R;
    bool isReloading = false;
    Charger ammoReloading = new Charger(2f, false);
    public float reloadingTime = 1f;
    public AudioSource leftCannonAudio = null;
    public AudioSource rightCannonAudio = null;
    public AudioClip fireAudio = null;

    [Header("Shield Components")]
    public GameObject energyShieldPrefab;
    public KeyCode shieldButton;
    public float shieldCost;

    [Header("UI Components")]
    public UI_Notice cursorNoticer;
    public Slider reloadingUI;
    public UI_IconBar bulletCountUI;

    private void Start()
    {
        leftCannonCharger = new Charger(0.28f);
        rightCannonCharger = new Charger(0.28f);
        switchCannon = new Charger(0.14f);

        if (fireAudio && leftCannonAudio && rightCannonAudio)
        {
            leftCannonAudio.clip = fireAudio;
            rightCannonAudio.clip = fireAudio;
        }
    }

    private void Update()
    {
        AdjustCannonAngles();
        InfomationUpdate();
    }
    private void FixedUpdate()
    {
        CannonSystemUpdate();
    }

    void AdjustCannonAngles()
    {
        // Adjust Left
        float oppo = cursorLocation.position.y - leftCannon.position.y;
        float adj = cursorLocation.position.x - leftCannon.position.x;
        float angle = Mathf.Rad2Deg * Mathf.Atan(oppo / adj);
        leftCannon.eulerAngles = Vector3.forward * (angle - offset);

        // Adjust Right
        oppo = cursorLocation.position.y - rightCannon.position.y;
        adj = cursorLocation.position.x - rightCannon.position.x;
        angle = Mathf.Rad2Deg * Mathf.Atan(oppo / adj);
        rightCannon.eulerAngles = Vector3.forward * (angle + offset);
    }

    void CannonSystemUpdate()
    {
        // Fire
        leftCannonCharger.FixedCharge();
        rightCannonCharger.FixedCharge();
        switchCannon.FixedCharge();

        // Reload
        if (isReloading)
        {
            ammoReloading.FixedCharge();
            if (ammoReloading.IsCharged())
            {
                bulletAmmo = ammoMagazine;
                ammoReloading.ReleaseCharge();
                if (cursorNoticer)
                {
                    cursorNoticer.WriteNotice("Reloaded!", Color.green, 0.5f);
                }
                reloadingUI.gameObject.SetActive(false);
                isReloading = false;
                return;
            }
            if (reloadingUI)
            {
                reloadingUI.gameObject.SetActive(true);
                reloadingUI.value = ammoReloading.Progress();
            }
            if (cursorNoticer && cursorNoticer.IsIdle())
            {
                cursorNoticer.WriteNotice("Reloading...", Color.yellow, -1);
            }
        }
    }

    public void InfomationUpdate()
    {
        if (bulletCountUI)
        {
            bulletCountUI.ChangeBarValue(bulletAmmo);
        }
    }

    // Player Control Command
    public void FireCannons()
    {
        if (bulletAmmo > 0 && isReloading == false)
        {
            if (switchCannon.IsCharged() && leftCannonCharger.IsCharged())
            {
                Fire(-1);
                leftCannonCharger.ReleaseCharge();
                switchCannon.ReleaseCharge();
                bulletAmmo--;
            }
            if (switchCannon.IsCharged() && rightCannonCharger.IsCharged())
            {
                Fire(1);
                rightCannonCharger.ReleaseCharge();
                switchCannon.ReleaseCharge();
                bulletAmmo--;
            }
        }
        if (bulletAmmo == 0)
        {
            ReloadCannons();
        }
    }
    public void ReloadCannons()
    {
        if (isReloading == false)
        {
            if (bulletAmmo < ammoMagazine)
            {
                isReloading = true;
                ammoReloading.chargingTime = reloadingTime * 0.2f + (1f - (float)bulletAmmo / (float)ammoMagazine) * reloadingTime * 0.8f;
            }
        }
    }


    void Fire(int cannonNumber = 0) // cannonIndex: =0 both, <0 left, >0 right
    {
        FC_CannonBullet b;
        if (cannonNumber <= 0)
        {
            b = (FC_CannonBullet)Instantiate(bullet, leftMuzzle.position, Quaternion.identity, bulletCollector);
            b.InitBullet(cursorLocation.position, firePower);
            if (fireAudio && leftCannonAudio)
            {
                leftCannonAudio.Play();
            }

        }
        if (cannonNumber >= 0)
        {
            b = (FC_CannonBullet)Instantiate(bullet, rightMuzzle.position, Quaternion.identity, bulletCollector);
            b.InitBullet(cursorLocation.position, firePower);
            if (fireAudio && rightCannonAudio)
            {
                rightCannonAudio.Play();
            }
        }
    }
}
