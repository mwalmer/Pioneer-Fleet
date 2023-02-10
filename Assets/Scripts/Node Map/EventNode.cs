using System;
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
        if(nodeState != NodeState.unvisited)
            return;
        
        _spriteRenderer.color = Color.cyan;
        Vector3 currentPosition = gameObject.transform.position;
        for (int i = 0; i < PlayerState.eventNodeList.Count; i++)
        {
            Vector3 nodePosition = PlayerState.eventNodeList[i].transform.position;
            // NodeState n = PlayerState.eventNodeList[i].GetComponent<EventNode>().nodeState;
            if (Vector3.Distance(currentPosition, nodePosition) < 3)
            {
                if(PlayerState.eventNodeList[i] == PlayerState.currentNode)
                    _lineRendererSpawner.SpawnSolid(currentPosition, nodePosition);
                else
                    _lineRendererSpawner.SpawnLine(currentPosition, nodePosition);
            }
        }
    }
    
    private void OnMouseExit()
    {
        UpdateColor();
        _lineRendererSpawner.ClearLines();
    }

    private void OnMouseDown()
    {
        Select();
        _lineRendererSpawner.ClearLines();
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
