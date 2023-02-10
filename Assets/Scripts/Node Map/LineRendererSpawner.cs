using System;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererSpawner : MonoBehaviour
{
    public GameObject dashPrefab;
    public GameObject solidPrefab;
    private List<GameObject> renderers;

    private void Awake()
    {
        renderers = new List<GameObject>();
    }

    public void SpawnSolid(Vector3 start, Vector3 end)
    {
        GameObject obj = Instantiate(solidPrefab);
        LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        renderers.Add(obj);
    }

    public void SpawnLine(Vector3 start, Vector3 end)
    {
        GameObject obj = Instantiate(dashPrefab);
        LineRenderer lineRenderer = obj.GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        renderers.Add(obj);
    }

    public void ClearLines()
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            Destroy(renderers[i]);
        }
        
        renderers.Clear();
    }
}
