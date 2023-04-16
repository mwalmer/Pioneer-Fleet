using UnityEngine;

public class loadSceneAfterDestruction : MonoBehaviour
{
    public GameObject[] objectsToDestroy;
   

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
        UI_CanvasInit.EnterNextScene(data.lastScene);
            }
        }
    }
}
