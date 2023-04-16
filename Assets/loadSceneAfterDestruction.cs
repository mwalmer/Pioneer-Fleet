using UnityEngine;

public class loadSceneAfterDestruction : MonoBehaviour
{
    public GameObject[] objectsToDestroy;
    //public FC_EndGameEvent fc;

    private bool allObjectsDestroyed = false;

    private void Update()
    {
        if (!allObjectsDestroyed)
        {
            bool anyObjectsRemaining = false;
            foreach (GameObject obj in objectsToDestroy)
            {
                if (obj != null)
                {
                    anyObjectsRemaining = true;
                    break;
                }
            }

            if (!anyObjectsRemaining)
            {
                allObjectsDestroyed = true;
                EventData data = EventData.GetData();
                FC_EndGameEvent.EnableEndGameEvent();
              //      fc.gameObject.SetActive(true);
              //  UI_CanvasInit.EnterNextScene(data.lastScene);
              // PlayerData.setMiniGameScore(100);
            }
        }
    }
}
