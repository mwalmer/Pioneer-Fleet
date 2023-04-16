using UnityEngine;

public class DestroyDetector : MonoBehaviour
{
    private void OnDestroy()
    {
        Debug.Log(FC_ScoreTaker.GetTotalScore());

        FC_ScoreTaker.AddScore("player destroyed",-400);
        FC_EndGameEvent.EnableEndGameEvent();
    }
}
