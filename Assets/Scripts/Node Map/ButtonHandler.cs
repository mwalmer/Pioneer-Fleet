using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public static void Travel()
    {
        NodeData.selectedNode.GetComponent<EventNode>().Travel();
    }
    
    public static void Cancel()
    {
        UI_WindowController controller = FindObjectOfType<UI_WindowController>();
        controller.FadeOut();

        if (NodeData.selectedNode == null)
            return;
        FindObjectOfType<GalaxyMap_CameraFocus>().SetFocus(NodeData.currentNode.transform);
        NodeData.selectedNode.GetComponent<EventNode>().UpdateColor();
        NodeData.selectedNode = null;
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().ClearLines();
        // NodeData.selectedNode = null;
    }
}
