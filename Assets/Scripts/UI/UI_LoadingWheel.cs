using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoadingWheel : MonoBehaviour
{
    public Transform trackingTarget;
    public bool testChecker;
    public Color wheelColor = new Color(1, 196f / 255f, 0, 1);
    public AudioClip loadingSound;
    public AudioClip finishingSound;
    public float successTime = 0.2f;
    private Slider wheel;
    private Image image;
    private float loadingTime = 1f;
    private float currentTime = -1;
    private bool isFinished = false;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        wheel = GetComponent<Slider>();
        audio = GetComponent<AudioSource>();
        image = GetComponentInChildren<Image>();
        if (image)
        {
            image.color = wheelColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (testChecker)
        {
            currentTime = 0;
            StartWheel(1f);
            testChecker = false;
        }

        if (trackingTarget)
        {
            transform.position = trackingTarget.position;
        }

        if (currentTime >= 0)
        {
            currentTime += Time.deltaTime;
            wheel.value = (currentTime / loadingTime);

            if (currentTime > loadingTime)
            {
                if (isFinished == false)
                {
                    audio.clip = finishingSound;
                    audio.Play();
                    isFinished = true;
                }

                wheel.value = 1;
                image.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, (currentTime - loadingTime) / successTime));
                if (currentTime - loadingTime > successTime)
                {
                    currentTime = -1;
                    wheel.value = 0;
                }
            }
        }
    }

    public void StartWheel(float _loadingTime)
    {
        loadingTime = _loadingTime;
        currentTime = 0;
        isFinished = false;
        image.color = wheelColor;
        audio.clip = loadingSound;
        audio.Play();
    }
    public bool IsFinished(bool resetAfterCheck)
    {
        if (resetAfterCheck)
        {
            isFinished = false;
            currentTime = -1;
            bool temp = isFinished;
            return temp;
        }
        return isFinished;
    }
}
