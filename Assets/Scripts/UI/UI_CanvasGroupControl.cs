using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CanvasGroupControl : MonoBehaviour
{
    public float appearingTime = 0.5f;
    private float currentAppearingTime = -1;
    private CanvasGroup group;
    public bool isAppeared = false;
    // Start is called before the first frame update
    void Start()
    {
        group = GetComponent<CanvasGroup>();
        group.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAppearingTime >= 0)
        {
            currentAppearingTime += Time.deltaTime;

            group.alpha = Mathf.Lerp(0, 1, currentAppearingTime / appearingTime);

            if (currentAppearingTime >= appearingTime)
            {
                currentAppearingTime = -1f;
                group.alpha = 1;
                isAppeared = true;
            }
        }
    }

    public void Show()
    {
        if (isAppeared == false && currentAppearingTime < 0)
        {
            currentAppearingTime = 0;
        }
    }
}
