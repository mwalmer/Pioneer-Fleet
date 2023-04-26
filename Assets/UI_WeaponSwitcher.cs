using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WeaponSwitcher : MonoBehaviour
{
    public List<UI_IconIndicator> indicators;

    [Header("Motion Status")]
    public float xOffset = 0;
    public float yOffset = 0;
    public float xAmp = 64;
    public float yAmp = 64;
    public bool isCountClockwise = false;
    public float alphaInterval = 0.2f;
    public float alphaOffset = 0.4f;
    public float maxAngle = 360;
    public int currentIndex;
    public int newIndex;
    private float currentTime = 0;
    public float animeTime = 0.5f;
    public bool testButton = false;

    // Start is called before the first frame update
    void Start()
    {
        ResetPosition();
        Debug.Log("WeaponSwitcher");
    }

    // Update is called once per frame
    void Update()
    {
        if (testButton == true)
        {
            testButton = false;
            NextIndicator();
        }

        if (indicators.Count > 0 && newIndex != currentIndex)
        {
            Debug.Log("Animating");
            WeaponSwitcherAnimation();
        }
    }
    void WeaponSwitcherAnimation()
    {
        if (currentTime >= 0)
        {
            currentTime += Time.deltaTime;

            AnimationInterval();

            if (currentTime >= animeTime)
            {
                currentTime = -1;
                currentIndex = newIndex;
            }
        }
    }

    public void ResetPosition()
    {
        float newX;
        float newY;
        float angularInterval = maxAngle / indicators.Count;
        for (int i = 0, index = currentIndex; i < indicators.Count; i++)
        {
            index = currentIndex + i;
            index = index >= indicators.Count ? index - indicators.Count : index;
            indicators[index].transform.SetSiblingIndex(indicators.Count - 1 - i);
            newX = Mathf.Cos(angularInterval * i * Mathf.Deg2Rad) * xAmp + xOffset;
            newY = Mathf.Sin(angularInterval * i * Mathf.Deg2Rad) * yAmp + yOffset;
            indicators[index].ChangeLocation(new Vector2(newX, newY));
            indicators[index].ChangeAlpha(1 - (i == 0 ? 0 : alphaOffset) - alphaInterval * i);
        }
    }

    void AnimationInterval()
    {
        float newX;
        float newY;
        float angularInterval = maxAngle / indicators.Count * Mathf.Deg2Rad;
        for (int i = 0, index = newIndex; i < indicators.Count; i++)
        {
            index = newIndex + i;
            index = index >= indicators.Count ? index - indicators.Count : index;
            indicators[index].transform.SetSiblingIndex(indicators.Count - 1 - i);
            newX = Mathf.Cos(angularInterval * i) * xAmp + xOffset;
            newY = Mathf.Sin(angularInterval * i) * yAmp + yOffset;
            indicators[index].ChangeLocation(new Vector2(newX, newY), currentTime / animeTime);
            indicators[index].ChangeAlpha(1 - (i == 0 ? 0 : alphaOffset) - alphaInterval * i, currentTime / animeTime);
        }
    }

    public void StartAnimation()
    {
        currentTime = 0;
    }

    public void NextIndicator(bool willAnimate = true)
    {
        if (currentIndex != newIndex)
        {
            currentIndex = newIndex;
            ResetPosition();
        }

        newIndex = currentIndex + 1;
        newIndex = newIndex >= indicators.Count ? newIndex - indicators.Count : newIndex;
        if (willAnimate) StartAnimation();
    }


}
