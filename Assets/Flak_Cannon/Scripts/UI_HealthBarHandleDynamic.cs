using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBarHandleDynamic : MonoBehaviour
{
    public float timeForOneLoop = 1f;
    public float ratioOfExtensionDynamic = 1.2f;
    public float ratioOfColorDynamic = 0.8f;
    public Color targetColor = Color.white;
    private float currentTime = 0;
    RectTransform rect;
    Image img;
    Color originalColor;
    Vector3 orignalScale;


    // Start is called before the first frame update
    void Start()
    {
        if (!rect)
        {
            rect = GetComponent<RectTransform>();
        }
        if (rect)
        {
            orignalScale = rect.localScale;
        }
        if (!img)
        {
            img = GetComponent<Image>();
        }
        if (img)
        {
            originalColor = img.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        Dynamic();

        if (currentTime > timeForOneLoop)
        {
            currentTime = 0;
        }
    }

    void Dynamic()
    {
        float r, g, b;
        float halfTime = (timeForOneLoop / 2f);
        if (currentTime < timeForOneLoop / 2f)
        {
            r = Mathf.Lerp(originalColor.r, targetColor.r, currentTime / halfTime * ratioOfColorDynamic);
            g = Mathf.Lerp(originalColor.g, targetColor.g, currentTime / halfTime * ratioOfColorDynamic);
            b = Mathf.Lerp(originalColor.b, targetColor.b, currentTime / halfTime * ratioOfColorDynamic);
        }
        else
        {
            float currentTimeTemp = currentTime - timeForOneLoop / 2f;
            r = Mathf.Lerp(targetColor.r, originalColor.r, currentTimeTemp / halfTime + (1 - ratioOfColorDynamic));
            g = Mathf.Lerp(targetColor.g, originalColor.g, currentTimeTemp / halfTime + (1 - ratioOfColorDynamic));
            b = Mathf.Lerp(targetColor.b, originalColor.b, currentTimeTemp / halfTime + (1 - ratioOfColorDynamic));
        }
        img.color = new Color(r, g, b, originalColor.a);

        if (currentTime < timeForOneLoop / 2f)
        {
            rect.localScale = Vector3.Lerp(orignalScale, ratioOfExtensionDynamic * orignalScale, currentTime / halfTime);
        }
        else
        {
            rect.localScale = Vector3.Lerp(ratioOfExtensionDynamic * orignalScale, orignalScale, (currentTime - halfTime) / halfTime);
        }
    }


}
