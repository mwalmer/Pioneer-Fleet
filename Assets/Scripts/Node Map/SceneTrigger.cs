using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private void OnDestroy()
    {
        // disable NodeMap
        if(NodeData.nodeMap)
            NodeData.nodeMap.SetActive(false);
    }
}
