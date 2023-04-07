using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject swG;
    public GameObject edG;
    public UI_WindowController sideWindowController;
    public UI_WindowController eventDialogController;
    public UI_Selection eventDialogSelection;
    public bool travelFlag = false;
    public bool showOnce = true;
    public bool showDialogWindow = false;

    private void Start()
    {
        
        swG = GameObject.Find("UI_SideWindow");
        sideWindowController = swG.GetComponent<UI_WindowController>();
        
        edG = GameObject.Find("UI_EventDialog");
        eventDialogController = edG.GetComponent<UI_WindowController>();
        eventDialogSelection = GameObject.Find("Selections").GetComponent<UI_Selection>();
    }

    private void Update()
    {
        if(!showDialogWindow)
            edG.SetActive(false);
        
        if(!travelFlag)
            return;

        if (sideWindowController.uiGroup.alpha != 0)
            return;
        
        if (showOnce)
        {
            GameObject node = NodeData.selectedNode;
            node.GetComponent<EventNode>().SetEventDialog();
            eventDialogSelection.CleanSelections();
            eventDialogSelection.RegisterSelection("Continue", "Continue");
            showOnce = false;
        }

        if (eventDialogSelection.GetCurrentSelection() == null)
            return;
        
        showOnce = true;
        travelFlag = false;
        showDialogWindow = false;
        eventDialogSelection.CleanSelections();
        Continue();
    }

    public void ShowDialogBox()
    {
        travelFlag = true;
        sideWindowController.FadeOut();
        GameObject temp = NodeData.selectedNode;
        NodeData.selectedNode.GetComponent<EventNode>().UpdateColor();
        NodeData.selectedNode.GetComponent<EventNode>().UpdateTravelIndicator();
        NodeData.selectedNode = null;
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().SetSolid();
        GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>().ClearLines();
        NodeData.selectedNode = temp;
    }
    
    public void Continue()
    {
        // remove event dialog box
        if(eventDialogController.uiGroup.alpha != 0)
            eventDialogController.FadeOut();
        
        // travel to node
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
