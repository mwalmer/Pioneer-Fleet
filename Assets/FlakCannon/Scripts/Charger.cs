using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charger
{
    public float chargingTime;
    private float currentTime;
    private bool isCharged = false;

    public Charger(float _chargingTime, bool initChargingStatus = false)
    {
        chargingTime = _chargingTime;
        isCharged = initChargingStatus;
    }
    public void Charge()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= chargingTime)
        {
            isCharged = true;
        }
    }
    public float Progress()
    {
        return currentTime / chargingTime;
    }

    public void FixedCharge()
    {
        currentTime += Time.fixedDeltaTime;
        if (currentTime >= chargingTime)
        {
            isCharged = true;
        }
    }
    public bool IsCharged()
    {
        return isCharged;
    }
    public void SetFullCharge()
    {
        isCharged = true;
    }

    public void ReleaseCharge()
    {
        currentTime = 0;
        isCharged = false;
    }
}
