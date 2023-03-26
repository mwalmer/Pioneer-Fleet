using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VE_FadeOut : MonoBehaviour
{
    public float fadeoutTime; // in sec
    private SpriteRenderer sr;
    private Image img;
    private Mask mask;
    private Image maskImg;
    [SerializeField]
    private Color color;
    public bool disableWhenFinished = false;
    public bool destroyWhenFinished = false;

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
            img.material = null;
        }
        else
        {
            sr.material = null;
        }

        // Pure white color masking
        BuildMask();
    }

    // Update is called once per frame
    void Update()
    {
        float newA = maskImg.color.a - Time.deltaTime / fadeoutTime;

        if (maskImg)
        {
            maskImg.color = new Color(color.r, color.g, color.b, newA);
        }

        if (sr)
        {
            newA = sr.color.a - Time.deltaTime / fadeoutTime;
            if (newA <= 0)
            {
                Destroy(this.gameObject);
            }
            else
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newA);
            }
        }
        else if (img)
        {
            newA = img.color.a - Time.deltaTime / fadeoutTime;
            if (newA <= 0)
            {
                if (disableWhenFinished)
                    this.gameObject.SetActive(false);
                if (destroyWhenFinished)
                    Destroy(this.gameObject);
                else
                    Destroy(this);
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
            }
            else
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, newA);
            }
        }
    }
    void BuildMask()
    {
        mask = GetComponent<Mask>();
        if (!mask)
            mask = this.gameObject.AddComponent<Mask>();
        GameObject maskImgObj = new GameObject();
        maskImgObj.name = "FadeoutColorMask";
        maskImgObj.transform.parent = this.transform;
        maskImg = maskImgObj.AddComponent<Image>();
        maskImg.rectTransform.sizeDelta = new Vector2(1920, 1080);
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
}
