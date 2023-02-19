using System;
using System.Collections.Generic;
using TMPro;
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
    public GameObject panel;

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
        // show info panel
        GameObject obj = GameObject.Find("InfoPanel");
        if (obj != null)
        {
            //Debug.Log(obj.GetComponentsInChildren<TextMeshPro>().Length);
            obj.transform.position = new Vector3(960, 540, 0);
        }
        
        // change node color
        if(nodeState != NodeState.unvisited)
            return;
        
        _spriteRenderer.color = Color.magenta;
        Vector3 currentPosition = gameObject.transform.position;
        for (int i = 0; i < NodeData.eventNodeList.Count; i++)
        {
            Vector3 nodePosition = NodeData.eventNodeList[i].transform.position;
            NodeState n = NodeData.eventNodeList[i].GetComponent<EventNode>().nodeState;
            if(n == NodeState.completed)
                continue;
            if (Vector3.Distance(currentPosition, nodePosition) < 3)
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
        UpdateColor();
        _lineRendererSpawner.ClearLines();
        GameObject obj = GameObject.Find("InfoPanel");
        if (obj != null)
        {
            obj.transform.position = new Vector3(20000, 20000, 0);
        }
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
        NodeData.currentNode.GetComponent<EventNode>().nodeState = NodeState.completed;
        NodeData.currentNode.GetComponent<EventNode>().UpdateColor();
        
        NodeData.currentNode = gameObject;
        nodeState = NodeState.active;
        UpdateColor();
        _lineRendererSpawner.SetSolid();
    }
    
    private void UpdateColor()
    {
        _spriteRenderer.color = nodeColors[(int)nodeState];
    }
}
