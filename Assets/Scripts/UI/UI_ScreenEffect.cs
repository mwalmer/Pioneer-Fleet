using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_ScreenEffect : MonoBehaviour
{
    public static UI_ScreenEffect screenEffect;
    public Image screenGlass;
    Color glassColor;
    float glassFlashTime = 0.5f;
    float glassFlashPeakTime = 0.1f;
    float currentGlassTime = -1;

    public RectTransform screenUI;
    float currentBumpTime = -1;
    float bumpInterval = 0.1f;
    float bumpTime = 1f;
    float bumpAmp = 0.1f;
    Vector3 initialUIPos;


    // Start is called before the first frame update
    void Start()
    {
        screenEffect = this;
        screenGlass.color = Vector4.zero;

        initialUIPos = screenUI.position;
    }

    // Update is called once per frame
    void Update()
    {
        GlassEffectUpdate();
        ScreenBumpUpdate();
    }
    void GlassEffectUpdate()
    {
        if (currentGlassTime >= 0)
        {
            currentGlassTime += Time.deltaTime;

            if (currentGlassTime < glassFlashPeakTime)
            {
                screenGlass.color = new Color(screenGlass.color.r, screenGlass.color.g, screenGlass.color.b, Mathf.Lerp(0, 0.5f, currentGlassTime / glassFlashPeakTime));
            }
            else if (currentGlassTime > glassFlashPeakTime)
            {
                screenGlass.color = new Color(screenGlass.color.r, screenGlass.color.g, screenGlass.color.b, Mathf.Lerp(0.5f, 0, (currentGlassTime / glassFlashTime) - (glassFlashPeakTime / glassFlashTime)));
            }

            if (currentGlassTime > glassFlashTime)
            {
                currentGlassTime = -1;
                screenGlass.color = Vector4.zero;
            }
        }
    }
    void ScreenBumpUpdate()
    {
        if (currentBumpTime >= 0)
        {
            currentBumpTime += Time.deltaTime;
            bumpAmp = Mathf.Lerp(bumpAmp, 0, currentBumpTime / bumpTime);

            if ((currentBumpTime / bumpInterval) % 2 > 1)
            {
                screenUI.position = new Vector3(screenUI.position.x, initialUIPos.y + Mathf.Lerp(bumpAmp * 0.5f, bumpAmp * -0.5f, (currentBumpTime % bumpInterval / bumpInterval)), initialUIPos.z);
            }
            else if ((currentBumpTime / bumpInterval) % 2 > 0)
            {
                screenUI.position = new Vector3(screenUI.position.x, initialUIPos.y + Mathf.Lerp(bumpAmp * -0.5f, bumpAmp * 0.5f, (currentBumpTime % bumpInterval / bumpInterval)), initialUIPos.z);
            }
            if ((currentBumpTime / (bumpInterval * 2)) % 2 > 1)
            {
                screenUI.position = new Vector3(initialUIPos.x + Mathf.Lerp(bumpAmp * 0.5f, bumpAmp * -0.5f, (currentBumpTime % bumpInterval / bumpInterval)), screenUI.position.y, initialUIPos.z);
            }
            else if ((currentBumpTime / (bumpInterval * 2)) % 2 > 0)
            {
                screenUI.position = new Vector3(initialUIPos.x + Mathf.Lerp(bumpAmp * -0.5f, bumpAmp * 0.5f, (currentBumpTime % bumpInterval / bumpInterval)), screenUI.position.y, initialUIPos.z);
            }

            if (currentBumpTime > bumpTime)
            {
                currentBumpTime = -1;
                screenUI.position = initialUIPos;
            }
        }

    }

    void GlassFlash(Color flashColor, float flashTime = 2f, float flashPeakTime = 1f)
    {
        currentGlassTime = 0;
        screenGlass.color = flashColor;
        glassFlashTime = flashTime;
        glassFlashPeakTime = flashPeakTime;
    }
    void ScreenBump(float _amplitude, float _bumpInterval, float _bumpTime)
    {
        currentBumpTime = 0;
        bumpAmp = _amplitude;
        bumpInterval = _bumpInterval;
        bumpTime = _bumpTime;
    }
    void StopBump()
    {
        currentBumpTime = -1;
        screenUI.position = initialUIPos;
    }
    public static void ScreenGlassFlash(Color flashColor, float flashTime = 2f, float flashPeakTime = 1f)
    {
        screenEffect.GlassFlash(flashColor, flashTime, flashPeakTime);
    }
    public static void ScreenUIBump(float amplitude = 0.1f, float bumpInterval = 0.1f, float bumpTime = 1f)
    {
        screenEffect.ScreenBump(amplitude, bumpInterval, bumpTime);
    }
    public static void StopScreenBump()
    {
        screenEffect.StopBump();
    }
}
