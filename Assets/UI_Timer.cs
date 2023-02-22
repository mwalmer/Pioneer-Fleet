using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Timer : MonoBehaviour
{
    // Time elements
    float time; // in sec
    public bool isRunning = false;
    public bool isCountingDown = false;
    public float timeRemindingBoder = 30f; // -1 means not using the reminding function.
    // Render components
    TextMeshProUGUI timerTextMesh;

    void Start()
    {
        timerTextMesh = GetComponent<TextMeshProUGUI>();
        //RunCountDown(180); // Test
    }

    // Update is called once per frame
    void Update()
    {
        if (timerTextMesh)
        {
            timerTextMesh.text = TimeToString();
            if (timeRemindingBoder >= 0 && time < timeRemindingBoder)
            {
                timerTextMesh.color = Color.red;
            }
        }

        if (isRunning)
        {
            if (!isCountingDown)
                time += Time.deltaTime;
            else
                time -= Time.deltaTime;
            Debug.Log(Time.deltaTime);
            if (time < 0) time = 0;
        }
    }

    public void Reset()
    {
        time = 0;
    }
    public void Pause()
    {
        isRunning = false;
    }
    public void Run()
    {
        isRunning = true;
    }
    public void RunCountDown(int min, int sec)
    {
        RunCountDown(min * 60f + (float)sec);
    }
    public void RunCountDown(float sec)
    {
        isCountingDown = true;
        time = sec;
        Run();
    }
    public bool IsTime()
    {
        // IsTime only works when it is using counting down mode.
        return (isCountingDown && time <= 0 ? true : false);
    }

    // Data Processing
    string TimeToString()
    {
        int min = (int)time / 60;
        int sec = (int)time % 60;
        return (min < 10 ? "0" + min : min) + ":" + (sec < 10 ? "0" + sec : sec);
    }
}
