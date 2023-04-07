using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_EnergyShield : MonoBehaviour
{
    public float energy = 1000;
    public float cost = 50; // energy per sec
    SpriteRenderer sr;
    RectTransform rectT;
    Collider2D collider;
    public bool isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rectT = this.GetComponent<RectTransform>();
        collider = GetComponentInChildren<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            energy -= cost * Time.deltaTime;

            EnergyShieldOn();

            if (energy <= 0)
            {
                energy = 0;
                isActive = false;
                EnergyShieldFinish();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("!");
            FC_Enemyfighter ef = col.gameObject.GetComponentInParent<FC_Enemyfighter>();
            ef.isHarmful = false;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("!");
            FC_Enemyfighter ef = col.gameObject.GetComponentInParent<FC_Enemyfighter>();
            ef.isHarmful = true;
        }
    }
    public void InitEnergyShield(float _energy, float _cost)
    {
        isActive = true;
        energy = _energy;
        cost = _cost;
    }

    void EnergyShieldOn()
    {
        if (rectT)
        {
            rectT.localScale = new Vector3(100 + energy, 100 + energy, rectT.localScale.z);
        }

    }
    void EnergyShieldFinish()
    {

    }
}
