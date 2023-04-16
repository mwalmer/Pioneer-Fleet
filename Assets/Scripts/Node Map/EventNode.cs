using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    public Animator animator;
    
    private void Awake()
    {
        nodeColors = new[]{ unvisitedColor, activeColor, completedColor, failedColor };
        nodeState = NodeState.unvisited;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRendererSpawner = GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>();
        animator = GetComponent<Animator>();
        animator.speed = Random.Range(0.20f, 0.40f);
    }

    public void LoadIcon()
    {
        GameObject icon;
        switch (eventData.eventType)
        {
            case EventData.EventType.shop:
                icon = Instantiate(Resources.Load<GameObject>("Node Map/EventNoticeIcon_0"), transform);
                //_spriteRenderer.color = new Color(0.3f, 0.3f, 0.05f,1);
                break;
            case EventData.EventType.passive:
                icon = Instantiate(Resources.Load<GameObject>("Node Map/EventNoticeIcon_4"), transform);
                //_spriteRenderer.color = new Color(0.5f, 0.8f, 0.5f,1);
                break;
            default:
                icon = Instantiate(Resources.Load<GameObject>("Node Map/EventNoticeIcon_1"), transform);
                //_spriteRenderer.color = new Color(0.5f, 0.2f, 0.2f, 1);

                break;
        }
        
        

        Vector3 vec = transform.position;
        vec.y += 0.32f;
        icon.transform.position = vec;
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
        if(NodeData.currentNode == gameObject)
            return;
        
        // draws line to nearby nodes
        float travelDist = FindObjectOfType<NodeMap>().travelDist;
        Color c = new Color(1.0f, 1.0f, 0.2f, 1);
        _spriteRenderer.color = c;
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
        
        if(NodeData.selectedNode == null)
            return;
        
        if (NodeData.selectedNode.GetComponent<EventNode>().nodeState == NodeState.completed)
        {
            SetCompleted();
        }
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

        if (defer)
        {
            // TODO: set complete
            return;
        }
        
        HandleEvent();
    }

    public void SetEventDialog()
    {
        if (eventData.text == "")
        {
            Debug.LogError("no text to display");
            return;
        }
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
                    UI_CanvasInit.EnterNextScene("bomber test");
                else
                    UI_CanvasInit.EnterNextScene("BattleBridge");
            } break;
            case EventData.EventType.passive:
            {
                eventData.SetPassiveData();
            } break;
            case EventData.EventType.singleMinigame:
            {
                eventData.lastScene = "NodeMap";
                if(eventData.minigameType == EventData.Minigame.StarFighter)
                    UI_CanvasInit.EnterNextScene("fighter");
                else if(eventData.minigameType == EventData.Minigame.FlakCannon)
                    UI_CanvasInit.EnterNextScene("FlakCannon");
                else if(eventData.minigameType == EventData.Minigame.MatchingMinigame)
                    UI_CanvasInit.EnterNextScene("ArmamentsAlign");
            } break;
            case EventData.EventType.boss:
            {
                eventData.setFleetBattleData();
                
                UI_CanvasInit.EnterNextScene("BattleBridge");
                Destroy(NodeData.nodeMap);
                NodeData.eventNodeList.Clear();
            } break;
            case EventData.EventType.turncoat:
            {
                eventData.setFleetBattleData();
                UI_CanvasInit.EnterNextScene("BattleBridge");
            } break;
            case EventData.EventType.MatchingMinigame:
            {
                UI_CanvasInit.EnterNextScene("ArmamentsAlign");
            } break;
            case EventData.EventType.shop:
            {
                //Debug.LogError("Invalid capital ship!\n");
            } break;
            case EventData.EventType.completed:
            {
                // get random passive event
            } break;
            default:
            {
                //Debug.LogError("how did I get here?");
            } break;
        }
        
    }

    public void SetCompleted()
    {
        EventData tempData = new EventData();
        tempData.eventType = EventData.EventType.completed;
        tempData.description = "A familiar planet";
        tempData.text = "";
        eventData = tempData;
    }

    public void Select(bool ignoreCurrent = false)
    {
        if(NodeData.currentNode == gameObject && !ignoreCurrent)
            return;

        if(!ignoreCurrent && (NodeData.selectedNode == gameObject || NodeData.currentNode == gameObject))
            return;

        NodeData.selectedNode = gameObject;
        FindObjectOfType<GalaxyMap_CameraFocus>().SetFocus(transform);
    }
    
    public void UpdateColor()
    {
        if(eventData.eventType == EventData.EventType.turncoat)
            _spriteRenderer.color = new Color(1.0f, .3f, .3f);
        else if(eventData.eventType == EventData.EventType.boss)
            _spriteRenderer.color = Color.red;
        else
            _spriteRenderer.color = nodeColors[(int)nodeState];
    }

    public void UpdateTravelIndicator()
    {
        NodeData.travelIndicator.SetActive(true);
        NodeMap nm = FindObjectOfType<NodeMap>();
        
        NodeData.travelIndicator.transform.position = NodeData.currentNode.transform.position;
        // TODO: get rid of magic number
        float travelRadius = nm.travelDist / 4.5f;
        NodeData.travelIndicator.transform.localScale = new Vector3(travelRadius, travelRadius, 1);
    }
}