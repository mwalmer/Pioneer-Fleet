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
    string[] ships = new string[4];
    int[] prices = new int[4];
    string lastSelection = "";

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
            showOnce = false;
            GameObject node = NodeData.selectedNode;
            node.GetComponent<EventNode>().SetEventDialog();
            eventDialogSelection.CleanSelections();

            if (node.GetComponent<EventNode>().eventData.eventType == EventData.EventType.shop)
            {
                for(int i = 0; i < ships.Length; i++)
                {
                    if(i < 2)
                    {
                        ships[i] = PlayerData.getRandomCapitalShip();
                        prices[i] = (Resources.Load("FleetBattle/" + ships[i], typeof(CapitalShip)) as CapitalShip).price;
                    }
                    else
                    {
                        ships[i] = PlayerData.getRandomStarFighter();
                        prices[i] = (Resources.Load("FleetBattle/" + ships[i], typeof(StarFighter)) as StarFighter).price;
                    }
                }
                
                eventDialogSelection.RegisterSelection("0", ships[0] + ": price " + prices[0]);
                eventDialogSelection.RegisterSelection("1", ships[1] + ": price " + prices[1]);
                eventDialogSelection.RegisterSelection("2", ships[2] + ": price " + prices[2]);
                eventDialogSelection.RegisterSelection("3", ships[3] + ": price " + prices[3]);
                eventDialogSelection.RegisterSelection("Exit", "exit");
            }
            else
            {
                eventDialogSelection.RegisterSelection("Continue", "Continue");
            }
            
        }

        if (eventDialogSelection.GetCurrentSelection() == null || eventDialogSelection.GetCurrentSelection() == lastSelection)
            return;
    

        GameObject n = NodeData.selectedNode;
        if(n.GetComponent<EventNode>().eventData.eventType == EventData.EventType.shop)
        {
            PlayerData pd = FindObjectOfType<PlayerData>();
            string selection = eventDialogSelection.GetCurrentSelection();
            switch(selection)
            {
                case "0":
                if(pd.currency < prices[0])
                {
                    lastSelection = selection;
                    return;  
                }
                pd.addPlayerCaptialShip(ships[0]);
                pd.currency -= prices[0];
                break;
                case "1":
                if(pd.currency < prices[1])
                {
                    lastSelection = selection;
                    return;  
                }
                pd.addPlayerCaptialShip(ships[1]);
                pd.currency -= prices[1];
                break;
                case "2":
                if(pd.currency < prices[2])
                {
                    lastSelection = selection;
                    return;  
                }
                pd.addPlayerStarFighter(ships[2]);
                pd.currency -= prices[2];
                break;
                case "3":
                if(pd.currency < prices[3])
                {
                    lastSelection = selection;
                    return;  
                }
                pd.addPlayerStarFighter(ships[3]);
                pd.currency -= prices[3];
                break;
                default:
                break;
            }
        }

        showOnce = true;
        travelFlag = false;
        showDialogWindow = false;

        lastSelection = "";
        eventDialogSelection.CleanSelections();
        Continue();
    }

    public void ShowDialogBox()
    {
        if (NodeData.selectedNode.GetComponent<EventNode>().eventData.text == "")
        {
            showOnce = true;
            travelFlag = false;
            showDialogWindow = false;

            lastSelection = "";
            eventDialogSelection.CleanSelections();
            sideWindowController.FadeOut();
            
            Continue();
            return;
        }
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
