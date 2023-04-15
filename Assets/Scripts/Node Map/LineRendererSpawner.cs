using System;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSpawner : MonoBehaviour
{
    public GameObject dashPrefab;
    public GameObject solidPrefab;
    private List<GameObject> dashRenderers;
    private List<GameObject> solidRenderers;
    private GameObject tempLine;

    private void Awake()
    {
        dashRenderers = new List<GameObject>();
        solidRenderers = new List<GameObject>();
    }

    public void SetSolid()
    {
        if (tempLine != null)
        {
            GameObject obj = Instantiate(tempLine, transform);
            obj.name = "path line";
            Color c = EventNode.nodeColors[(int)EventNode.NodeState.completed];
            c.a = 0.5f;
            obj.GetComponent<LineRenderer>().startColor = c;
            obj.GetComponent<LineRenderer>().endColor = c;
            solidRenderers.Add(obj);
        }
    }

    public void SpawnSolid(Vector3 start, Vector3 end)
    {
        if(NodeData.selectedNode != null)
            return;
        GameObject obj = Instantiate(solidPrefab, transform);
        obj.name = "solid line";
        LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        tempLine = obj;
    }

    public void SpawnLine(Vector3 start, Vector3 end)
    {
        if(NodeData.selectedNode != null)
            return;

        GameObject obj = Instantiate(dashPrefab, transform);
        obj.name = "dashed line";
        LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        dashRenderers.Add(obj);
    }

    // only clears lines if the selected nodes is null!
    public void ClearLines()
    {
        if(NodeData.selectedNode != null)
            return;
        
        for (int i = 0; i < dashRenderers.Count; i++)
        {
            Destroy(dashRenderers[i]);
        }
        Destroy(tempLine);
        dashRenderers.Clear();
    }
}
