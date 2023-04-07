using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public UI_WindowController sideWindowController;
    public UI_WindowController eventDialogController;
    public UI_Selection eventDialogSelection;
    public bool travelFlag = false;
    public bool showOnce = true;

    private void Start()
    {
        GameObject sw = GameObject.Find("UI_SideWindow");
        sideWindowController = sw.GetComponent<UI_WindowController>();
        
        GameObject ed = GameObject.Find("UI_EventDialog");
        eventDialogController = ed.GetComponent<UI_WindowController>();
        eventDialogSelection = GameObject.Find("Selections").GetComponent<UI_Selection>();
    }

    private void Update()
    {
        if(!travelFlag)
            return;

        if (sideWindowController.uiGroup.alpha != 0)
            return;
        
        if (showOnce)
        {
            GameObject node = NodeData.selectedNode;
            node.GetComponent<EventNode>().ShowEventDialog();
            showOnce = false;
        }

        if (eventDialogSelection.GetCurrentSelection() == null)
            return;
        
        showOnce = true;
        travelFlag = false;
        eventDialogSelection.CleanSelections();
        Continue();
    }

    public void Travel()
    {
        travelFlag = true;
        sideWindowController.FadeOut();
        GameObject temp = NodeData.selectedNode;
        NodeData.selectedNode.GetComponent<EventNode>().UpdateColor();
        NodeData.selectedNode = null;
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().SetSolid();
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().ClearLines();
        NodeData.selectedNode = temp;
    }
    
    public void Continue()
    {
        GameObject node = NodeData.selectedNode;
        node.GetComponent<EventNode>().Travel();
    }
    
    public void Cancel()
    {
        sideWindowController.FadeOut();

        if (NodeData.selectedNode == null)
            return;
        FindObjectOfType<GalaxyMap_CameraFocus>().SetFocus(NodeData.currentNode.transform);
        NodeData.selectedNode.GetComponent<EventNode>().UpdateColor();
        NodeData.selectedNode = null;
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().ClearLines();
    }
}
