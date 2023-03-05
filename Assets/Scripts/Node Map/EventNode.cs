using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public string planetDescription;

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
        NodeData.ui.SetActive(true);
        NodeData.title.GetComponent<TextMeshProUGUI>().SetText("Planet: " + planetName);
        NodeData.description.GetComponent<TextMeshProUGUI>().SetText(planetDescription);
        
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
        NodeData.ui.SetActive(false);
    }

    private void OnMouseDown()
    {
        Select();
        
        _lineRendererSpawner.ClearLines();
    }

    public void Select(bool defer=false)
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
        
        if(defer) 
            return;
        LoadScene(2);
    }
    
    private void UpdateColor()
    {
        _spriteRenderer.color = nodeColors[(int)nodeState];
    }
    
    private void LoadScene(int id)
    {
        // disable NodeMap stuff
        if(!NodeData.nodeMap)
            NodeData.nodeMap = GameObject.Find("NodeMap");
        NodeData.nodeMap.SetActive(false);
        
        // load scene additively
        SceneManager.LoadScene(id, LoadSceneMode.Additive);
    }
}
