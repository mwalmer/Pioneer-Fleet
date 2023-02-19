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
            obj.GetComponent<LineRenderer>().startColor = EventNode.nodeColors[(int)EventNode.NodeState.completed];
            obj.GetComponent<LineRenderer>().endColor = EventNode.nodeColors[(int)EventNode.NodeState.completed];
            solidRenderers.Add(obj);
        }
    }

    public void SpawnSolid(Vector3 start, Vector3 end)
    {
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
        GameObject obj = Instantiate(dashPrefab, transform);
        obj.name = "dashed line";
        LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        dashRenderers.Add(obj);
    }

    public void ClearLines()
    {
        for (int i = 0; i < dashRenderers.Count; i++)
        {
            Destroy(dashRenderers[i]);
        }
        Destroy(tempLine);
        dashRenderers.Clear();
    }
}
