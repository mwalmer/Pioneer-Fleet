using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VE_FadeIn : MonoBehaviour
{
    public float fadeinTime = 1f; // in sec
    public bool isSolidFadein = false;
    private SpriteRenderer sr;
    private Image img;
    private Mask mask;
    private Image maskImg;
    private Color color = Color.white;
    private Material backupMaterial;

    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        if (!sr)
        {
            img = GetComponent<Image>();
            if (!img)
            {
                Destroy(this.gameObject);
                return;
            }
            backupMaterial = img.material;
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
            img.material = null; // Since prebuilt material cannot be the mask parent, clean the material.
        }
        else
        {
            backupMaterial = sr.material;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
            sr.material = null;
        }

        // Pure white color masking
        BuildMask();
    }

    // Update is called once per frame
    void Update()
    {
        float newA = 0;

        if (maskImg)
        {
            newA = maskImg.color.a - Time.deltaTime / fadeinTime;
            if (newA < 0)
            {
                StopVE();
                return;
            }
            maskImg.color = new Color(color.r, color.g, color.b, newA);
        }

        if (!isSolidFadein)
        {
            if (sr)
            {
                newA = sr.color.a + Time.deltaTime * fadeinTime;
                if (newA > 0)
                {
                    sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newA);
                }
            }
            else if (img)
            {
                newA = img.color.a + Time.deltaTime * fadeinTime;
                if (newA > 0)
                {
                    img.color = new Color(img.color.r, img.color.g, img.color.b, newA);
                }
            }
        }
        else
        {
            if (sr)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1);
            }
            else if (img)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
            }
        }
    }

    public void ChangeMaterial(Material _material)
    {
        if (sr)
        {
            sr.material = _material;
        }
        else if (img)
        {
            img.material = _material;
        }
    }
    public void ChangeColor(Color _color)
    {
        color = _color;
    }
    void BuildMask()
    {
        mask = GetComponent<Mask>();
        if (!mask)
            mask = this.gameObject.AddComponent<Mask>();
        GameObject maskImgObj = new GameObject();
        maskImgObj.name = "FadeInColorMask";
        maskImgObj.transform.parent = this.transform;
        maskImg = maskImgObj.AddComponent<Image>();
        maskImg.rectTransform.sizeDelta = new Vector2(1920, 1080);
    }
    void RemoveMask()
    {
        if (sr)
        {
            sr.material = backupMaterial;
        }
        else if (img)
        {
            img.material = backupMaterial;
        }
        Destroy(maskImg.gameObject);
    }
    void RemoveVE()
    {
        Destroy(this);
    }
    public void StopVE()
    {
        RemoveMask();
        RemoveVE();
    }
    public void SetSolidFadein(bool _isSolidFadein)
    {
        isSolidFadein = _isSolidFadein;
    }
}
