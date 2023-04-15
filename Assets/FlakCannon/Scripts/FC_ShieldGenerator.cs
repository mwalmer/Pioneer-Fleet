using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_ShieldGenerator : MonoBehaviour
{
    public GameObject shieldPrefab;
    public FC_ShieldChargeIndicator chargeIndicator;
    public UI_IconBar energyIndicator;
    public float cooldown = 1f;
    public float cost = 1f;
    public float maxChargeTime = 3f;
    public float energyThreshold = 250;
    public float maxEnergy = 1000;
    private float chargedEnergy = 0f;
    private RectTransform rect;
    public float maxHoldTime = 5f;
    private float currentHoldTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        InitateShieldComponents();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHoldTime >= 0)
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime > maxHoldTime)
            {
                currentHoldTime = -1;
                chargedEnergy = 0;
                chargeIndicator.ReleaseShieldFailed();
                InitateShieldComponents();
                return;
            }
        }
    }

    public void ChargingShield()
    {
        if (currentHoldTime < 0) currentHoldTime = 0;

        chargedEnergy += (Time.deltaTime / maxChargeTime) * maxEnergy;

        if (FC_GameManager.GameManager.playerEnergy < 2)
        {
            if (chargedEnergy >= (FC_GameManager.GameManager.playerEnergy + 1) * (maxEnergy / 3))
            {
                chargedEnergy = (FC_GameManager.GameManager.playerEnergy + 1) * (maxEnergy / 3);
            }
        }

        if (chargedEnergy > maxEnergy) chargedEnergy = maxEnergy;

        if (chargeIndicator)
        {
            chargeIndicator.SetValue(chargedEnergy / maxEnergy);
            chargeIndicator.ShowMarks();
        }
    }
    public void GenerateShield(Vector2 aimmingPos)
    {
        if (chargedEnergy < energyThreshold)
        {
            chargedEnergy = 0;
            chargeIndicator.ReleaseShieldFailed();
            InitateShieldComponents();
            return;
        }

        FC_GameManager.GameManager.playerEnergy -= (chargedEnergy >= 500 ? (chargedEnergy >= 1000 ? 2 : 1) : 0);
        energyIndicator.ChangeBarValue(FC_GameManager.GameManager.playerEnergy);

        GameObject temp = Instantiate(shieldPrefab, rect);
        temp.name = "Shield!";
        temp.transform.position = aimmingPos;
        FC_EnergyShield eShield = temp.GetComponent<FC_EnergyShield>();
        eShield.InitEnergyShield(chargedEnergy >= (maxEnergy * 2 / 3) ? (chargedEnergy >= maxEnergy ? 1000 : 600) : 300);
        InitateShieldComponents();
    }

    public void InitateShieldComponents()
    {
        chargedEnergy = 0;
        if (chargeIndicator)
        {
            chargeIndicator.SetValue(0);
            chargeIndicator.OffMarks();
        }

    }
}
