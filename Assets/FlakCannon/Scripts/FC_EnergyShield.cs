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
    private float visualSize = 125f;
    private float initialAlpha = 1f;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rectT = this.GetComponent<RectTransform>();
        collider = GetComponentInChildren<Collider2D>();
        initialAlpha = sr.color.a;
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
            ef.blockedByIt = this;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            FC_Enemyfighter ef = col.gameObject.GetComponentInParent<FC_Enemyfighter>();
            ef.isHarmful = true;
            if (ef.blockedByIt == this)
            {
                ef.blockedByIt = null;
            }
        }
    }
    public void InitEnergyShield(float _energy, float _cost = 200)
    {
        isActive = true;
        energy = _energy;
        visualSize = energy / 2f;
        cost = _cost;
        EnergyShieldOn();
    }

    void EnergyShieldOn()
    {
        if (rectT)
        {
            if (energy <= visualSize)
            {
                rectT.localScale = new Vector3(energy + visualSize, energy + visualSize, rectT.localScale.z);
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, initialAlpha * (energy / visualSize));
            }
            else
            {
                rectT.localScale = new Vector3(visualSize * 2, visualSize * 2, rectT.localScale.z);
            }
        }

    }
    void EnergyShieldFinish()
    {
        Destroy(this.gameObject);
    }
}
