using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public static void Travel()
    {
        Debug.Log("pressed");
        // NodeData.selectedNode.GetComponent<EventNode>().Travel();
    }
    
    public static void Cancel()
    {
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().ClearLines();
        // NodeData.selectedNode = null;
    }
}
