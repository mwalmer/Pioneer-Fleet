using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventNode : MonoBehaviour
{
    public enum NodeState
    {
        unvisited = 0,
        active = 1,
        completed = 2,
        failed = 3
    }

    public string planetName;
    public EventData eventData;
    public bool turncoat;
    

    public NodeState nodeState;
    private SpriteRenderer _spriteRenderer;

    public static Color[] nodeColors;
    public Color unvisitedColor;
    public Color activeColor;
    public Color completedColor;
    public Color failedColor;
    public LineRendererSpawner _lineRendererSpawner;
    
    private void Awake()
    {
        nodeColors = new[]{ unvisitedColor, activeColor, completedColor, failedColor };
        nodeState = NodeState.unvisited;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRendererSpawner = GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>();
    }

    private void Start()
    {
        switch (eventData.nodeType)
        {
            case EventData.NodeType.asteroid:
                // _spriteRenderer.sprite = 
                break;
            case EventData.NodeType.star:
                break;
        }
    }

    private void OnMouseEnter()
    {
        if(NodeData.selectedNode != null)
            return;
        
        // change node color
        if(nodeState != NodeState.unvisited)
            return;
        
        // draws line to nearby nodes
        float travelDist = FindObjectOfType<NodeMap>().travelDist;
        _spriteRenderer.color = Color.magenta;
        Vector3 currentPosition = gameObject.transform.position;
        for (int i = 0; i < NodeData.eventNodeList.Count; i++)
        {
            Vector3 nodePosition = NodeData.eventNodeList[i].transform.position;
            NodeState n = NodeData.eventNodeList[i].GetComponent<EventNode>().nodeState;
            if(n == NodeState.completed)
                continue;
            if (Vector3.Distance(currentPosition, nodePosition) < travelDist)
            {
                if(NodeData.eventNodeList[i] == NodeData.currentNode)
                    _lineRendererSpawner.SpawnSolid(currentPosition, nodePosition);
                else
                    _lineRendererSpawner.SpawnLine(currentPosition, nodePosition);
            }
        }
    }
    
    private void OnMouseExit()
    {
        if(NodeData.selectedNode != null)
            return;
        UpdateColor();
        _lineRendererSpawner.ClearLines();
    }

    private void OnMouseDown()
    {
        if(NodeData.selectedNode != null)
            return;
        
        Select();
        _lineRendererSpawner.ClearLines();
        
        // show ui
        GameObject sw = FindObjectOfType<ButtonHandler>().swG;
        UI_LocationInfo info = sw.GetComponent<UI_LocationInfo>();
        UI_WindowController sideWindowController = sw.GetComponent<UI_WindowController>();
        
        info.ChangeName(planetName, Color.white);
        info.ChangeDescription(eventData.description, Color.white);
        sideWindowController.FadeBack();
    }

    public void Travel(bool defer=false)
    {
        if(NodeData.selectedNode == null)
            return;

        //TODO: this should be done when the player wins/loses
        NodeData.currentNode.GetComponent<EventNode>().nodeState = NodeState.completed;
        NodeData.currentNode.GetComponent<EventNode>().UpdateColor();
        if(eventData.eventType == EventData.EventType.turncoat)
            NodeData.bossNode.SetActive(true);
        
        NodeData.currentNode = gameObject;
        nodeState = NodeState.active;
        UpdateColor();
        _lineRendererSpawner.SetSolid();
        UpdateTravelIndicator();

        NodeData.selectedNode = null;
        _lineRendererSpawner.ClearLines();
        
        if(defer) 
            return;

        HandleEvent();
    }

    public void SetEventDialog()
    {
        // event dialog ui
        var bh = FindObjectOfType<ButtonHandler>();
        GameObject ed = bh.edG;
        ed.SetActive(true);
        bh.showDialogWindow = true;
        UI_EventDialog eventDialog = bh.edG.GetComponent<UI_EventDialog>();
        UI_WindowController eventDialogController = bh.eventDialogController;
        eventDialogController.FadeBack();
        eventDialog.ChangeName(planetName, Color.white);
        eventDialog.ChangeDescription(eventData.text, Color.white);
        
    }

    private void HandleEvent()
    {
        eventData.SetData();

        switch (eventData.eventType)
        {
            case EventData.EventType.battle:
            {
                eventData.setFleetBattleData();
                if(eventData.minigameType == EventData.Minigame.Bomber)
                    LoadScene(6);
                else
                    LoadScene(2);
            } break;
            case EventData.EventType.passive:
            {
                eventData.SetPassiveData();
            } break;
            case EventData.EventType.singleMinigame:
            {
                eventData.lastScene = "NodeMap";
                if(eventData.minigameType == EventData.Minigame.StarFighter)
                    LoadScene(3);
                else if(eventData.minigameType == EventData.Minigame.FlakCannon)
                    LoadScene(4);
                else if(eventData.minigameType == EventData.Minigame.MatchingMinigame)
                    LoadScene(5);
            } break;
            case EventData.EventType.boss:
            {
                eventData.setFleetBattleData();
                // NodeData.newMap = true;
            
                LoadScene(2);
                Destroy(NodeData.nodeMap);
                NodeData.eventNodeList.Clear();
            } break;
            case EventData.EventType.turncoat:
            {
                eventData.setFleetBattleData();
                LoadScene(2);
            } break;
            case EventData.EventType.MatchingMinigame:
            {
                LoadScene(5);
            } break;
            default:
            {
                Debug.LogError("how did I get here?");
            } break;
        }
    }

    public void Select(bool ignoreCurrent = false)
    {
        if(nodeState != NodeState.unvisited)
            return;

        if(!ignoreCurrent && (NodeData.selectedNode == gameObject || NodeData.currentNode == gameObject))
            return;

        NodeData.selectedNode = gameObject;
        FindObjectOfType<GalaxyMap_CameraFocus>().SetFocus(transform);
    }
    
    public void UpdateColor()
    {
        if(eventData.eventType == EventData.EventType.turncoat)
            _spriteRenderer.color = Color.red;
        else if(eventData.eventType == EventData.EventType.boss)
            _spriteRenderer.color = Color.blue;
        else
            _spriteRenderer.color = nodeColors[(int)nodeState];
    }
    
    private void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void UpdateTravelIndicator()
    {
        NodeData.travelIndicator.SetActive(true);
        NodeMap nm = FindObjectOfType<NodeMap>();
        SpriteRenderer sr = NodeData.travelIndicator.GetComponent<SpriteRenderer>();
        
        NodeData.travelIndicator.transform.position = NodeData.currentNode.transform.position;
        // TODO: get rid of magic number
        float travelRadius = nm.travelDist / 4.5f;
        NodeData.travelIndicator.transform.localScale = new Vector3(travelRadius, travelRadius, 1);
        // sp.sprite = new Vector2(.5f, .5f);
    }
}