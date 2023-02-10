using System.Collections.Generic;
using UnityEngine;

public class EventNode : MonoBehaviour
{
    public enum NodeState
    {
        unvisited = 0,
        active = 1,
        completed = 2,
        failed = 3
    }
    
    public NodeState nodeState;
    public Transform _transform;
    private SpriteRenderer _spriteRenderer;

    public static Color[] nodeColors;
    public Color unvisitedColor;
    public Color activeColor;
    public Color completedColor;
    public Color failedColor;
    public static PathRenderer pathRenderer;

    
    public static List<Vector3> reachableNodes = new List<Vector3>();
    public static List<NodeState> reachableNodeStates = new List<NodeState>();

    private void Awake()
    {
        nodeColors = new[]{ unvisitedColor, activeColor, completedColor, failedColor };
        nodeState = NodeState.unvisited;
        _transform = GetComponent<Transform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        pathRenderer = GameObject.Find("PathRenderer").GetComponent<PathRenderer>();
    }

    private void OnMouseEnter()
    {
        if(nodeState != NodeState.unvisited)
            return;
        
        _spriteRenderer.color = Color.cyan;

        // reachableNodes.Add();
        Vector3 currentPosition = gameObject.transform.position;
        for (int i = 0; i < PlayerState.eventNodeList.Count; i++)
        {
            Vector3 nodePosition = PlayerState.eventNodeList[i].transform.position;
            NodeState n = PlayerState.eventNodeList[i].GetComponent<EventNode>().nodeState;
            if (Vector3.Distance(currentPosition, nodePosition) < 3)
            {
                reachableNodes.Add(currentPosition);
                reachableNodes.Add(nodePosition);
                reachableNodeStates.Add(n);
            }
        }
        
        pathRenderer.render();
    }
    
    private void OnMouseExit()
    {
        UpdateColor();
        pathRenderer.clear();
        reachableNodes.Clear();
        reachableNodeStates.Clear();
    }

    private void OnMouseDown()
    {
        Select();
        pathRenderer.clear();
        reachableNodes.Clear();
        reachableNodeStates.Clear();
    }

    public void Select()
    {
        if(nodeState != NodeState.unvisited)
            return;
        
        //TODO: this should be done when the player wins/loses
        PlayerState.currentNode.GetComponent<EventNode>().nodeState = NodeState.completed;
        PlayerState.currentNode.GetComponent<EventNode>().UpdateColor();
        
        PlayerState.currentNode = gameObject;
        nodeState = NodeState.active;
        UpdateColor();
    }
    
    private void UpdateColor()
    {
        _spriteRenderer.color = nodeColors[(int)nodeState];
    }
}
