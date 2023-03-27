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
    
    public enum NodeType
    {
        planet = 0,
        asteroid = 1,
        star = 2
    }
    
    public enum EventType
    {
        planet = 0,
        asteroid = 1,
        star = 2
    }

    public string planetName;
    public EventData eventData;
    

    public NodeState nodeState;
    private SpriteRenderer _spriteRenderer;

    public static Color[] nodeColors;
    public Color unvisitedColor;
    public Color activeColor;
    public Color completedColor;
    public Color failedColor;
    private LineRendererSpawner _lineRendererSpawner;
    
    private void Awake()
    {
        nodeColors = new[]{ unvisitedColor, activeColor, completedColor, failedColor };
        nodeState = NodeState.unvisited;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRendererSpawner = GameObject.Find("LineRenderer Spawner").GetComponent<LineRendererSpawner>();
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
        UI_WindowController controller = FindObjectOfType<UI_WindowController>();
        UI_LocationInfo info = FindObjectOfType<UI_LocationInfo>();
        info.ChangeName(planetName, Color.white);
        info.ChangeDescription(eventData.description, Color.white);
        controller.FadeBack();
    }

    public void Travel(bool defer=false)
    {
        if(NodeData.selectedNode == null)
            return;

        //TODO: this should be done when the player wins/loses
        NodeData.currentNode.GetComponent<EventNode>().nodeState = NodeState.completed;
        NodeData.currentNode.GetComponent<EventNode>().UpdateColor();
        
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

    private void HandleEvent()
    {
        if (eventData.eventType == EventData.EventType.battle)
        {
            eventData.SetData();
            LoadScene(2);
        }
        else if (eventData.eventType == EventData.EventType.passive)
        {
            Debug.Log(eventData.text);
            eventData.SetPassiveData();
            // TODO: show text box!
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
        _spriteRenderer.color = nodeColors[(int)nodeState];
    }
    
    private void LoadScene(int id)
    {
        SceneManager.LoadScene(id);
    }

    private void UpdateTravelIndicator()
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
