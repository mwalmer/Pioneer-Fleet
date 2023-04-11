using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChargeMarkAnimation : MonoBehaviour
{
    public AudioClip sfxWhenMarked;
    public float animeTime = 0.25f;
    Image mark;
    Color initialColor;
    bool isMarked = false;
    float currentTime = 0;
    RectTransform markRect;
    Vector2 initialSize;
    FC_ShieldChargeIndicator indicator;
    // Start is called before the first frame update
    void Awake()
    {
        mark = this.GetComponentInChildren<Image>();
        markRect = mark.GetComponent<RectTransform>();
        initialSize = markRect.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mark()
    {
        if (isMarked == false)
        {
            isMarked = true;
            // Play animation and effect for the first time enter
            if (indicator)
            {
                indicator.PlaySound(sfxWhenMarked);
            }

        }
        if (isMarked == true && currentTime < animeTime)
        {
            currentTime += Time.deltaTime;
            mark.color = Color.white;
            markRect.sizeDelta = new Vector2(initialSize.x, Mathf.Lerp(initialSize.y * 2.5f, initialSize.y * 1.4f, currentTime / animeTime));
        }
    }
    public void ChangeAngle(float _angle)
    {
        transform.rotation = Quaternion.Euler(0, 0, _angle * -1 + 180);
    }
    public void SetInitialColor(Color _color)
    {
        initialColor = _color;
        mark.color = _color;
    }
    public void Reset()
    {
        isMarked = false;
        currentTime = 0;
        mark.color = initialColor;
        markRect.sizeDelta = initialSize;
    }

    public void RegisterIndicator(FC_ShieldChargeIndicator _indicator)
    {
        indicator = _indicator;
    }

}
