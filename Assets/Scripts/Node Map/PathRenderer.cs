using System;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    public GameObject prefab;
    private List<GameObject> paths;
    private List<LineRenderer> renderers;

    private void Awake()
    {
        paths = new List<GameObject>();
        renderers = new List<LineRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject path = Instantiate(prefab);
            path.name = "path: " + i;
            LineRenderer lineRenderer = path.GetComponent<LineRenderer>();
            lineRenderer.startColor = EventNode.nodeColors[i];
            lineRenderer.endColor = EventNode.nodeColors[i];
            lineRenderer.widthMultiplier = 0.05f;
            lineRenderer.enabled = false;
            
            paths.Add(path);
            renderers.Add(lineRenderer);
        }
    }

    public void render()
    {
        List<List<Vector3>> points = new List<List<Vector3>>();
        points.Add(new List<Vector3>());
        points.Add(new List<Vector3>());
        points.Add(new List<Vector3>());
        points.Add(new List<Vector3>());
        
        for (int i = 0; i < EventNode.reachableNodeStates.Count; i++)
        {
            points[(int)EventNode.reachableNodeStates[i]].Add(EventNode.reachableNodes[2 * i]);
            points[(int)EventNode.reachableNodeStates[i]].Add(EventNode.reachableNodes[2 * i + 1]);
        }

        for (int i = 0; i < 4; i++)
        {
            renderers[i].positionCount = points[i].Count;
            renderers[i].SetPositions(points[i].ToArray());
            renderers[i].enabled = true;
        }
    }

    public void clear()
    {
        for (int i = 0; i < 4; i++)
        {
            renderers[i].enabled = false;
        }
    }
}
