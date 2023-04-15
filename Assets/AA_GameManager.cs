using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA_GameManager : MonoBehaviour
{
    public UI_Timer gameTimer;
    public FC_EndGameEvent gameEndEvent;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameTimer.IsTime())
        {
            // Time out event
            gameEndEvent.gameObject.SetActive(true);



        }
    }

    void Init()
    {
        if (gameTimer)
        {
            gameTimer.time = EventData.GetData().MT_timeGiven;
            gameTimer.isCountingDown = true;
            gameTimer.Run();
        }

    }
}
