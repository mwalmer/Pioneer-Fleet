using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class NodeMap : MonoBehaviour
{
    public float travelDist;
    public int numberOfNodes;
    public GameObject eventNodePrefab;
    public float minimumDistance;
    public float maximumDistance;
    public static GameObject instance;

    private void Awake()
    {
        EventPresets.InitPresets();
        if (instance != null)
        {
            FindObjectOfType<GalaxyMap_CameraFocus>().SetFocus(NodeData.currentNode.transform);
            Destroy(gameObject);
            return;
        }
        
        NodeData.travelIndicator = GameObject.Find("TravelIndicator");
        instance = gameObject;
        NodeData.nodeMap = instance;
        DontDestroyOnLoad(instance);
        
        GenerateNodes();
        NodeData.currentNode = NodeData.eventNodeList[0];
    }
    
    private void Start()
    {
        NodeData.travelIndicator.SetActive(false);
        NodeData.currentNode.GetComponent<EventNode>().Select(true);
        NodeData.selectedNode.GetComponent<EventNode>().Travel(true);
    }

    private void OnDestroy()
    {
        if(NodeData.nodeMap != gameObject)
            NodeData.nodeMap.SetActive(true);
    }

    private void GenerateNodes()
    {
        // Create/Place nodes
        Vector3 start = new Vector3(-4, 0, 0);
        Vector3 goal = new Vector3(4, 0, 0);
        Vector2 midpoint = new Vector2((goal.x + start.x) / 2, (goal.y + goal.y) / 2);
        
        CreateNode(start, "Start");
        CreateNode(goal, "Goal");
        
        for (int i = 0; i < numberOfNodes - 2; i++)
        {
            Vector3 position = new Vector3(0, 0, 0);
            if(!placeNode(ref position, midpoint))
                continue;
            CreateNode(position, i.ToString());
        }
        
        // Generate rng elements (names, descriptions, etc...)
        // TODO: temp
        int numNodes = NodeData.eventNodeList.Count;
        for (int i = 0; i < numNodes; i++)
        {
            EventNode node = NodeData.eventNodeList[i].GetComponent<EventNode>();
            node.planetName = NodeData.PlanetNames[Random.Range(0, NodeData.PlanetNames.Count)];
            
            // Event presets picked here
            node.eventData = EventPresets.presets[Random.Range(0, EventPresets.presets.Count)];
        }
    }

    private void CreateNode(Vector3 position, string str)
    {
        GameObject node = Instantiate(eventNodePrefab, position, new Quaternion(0, 0, 0, 0));
        node.transform.parent = gameObject.transform;
        node.name = "Event Node: " + str;
        NodeData.eventNodeList.Add(node);
    }

    private bool placeNode(ref Vector3 position, Vector2 center)
    {
        // trys to place the node X amount of time before giving up
        int trys = 1000;
        //TODO: add screen bounds checks
        Camera c = GameObject.Find("Main Camera").GetComponent<Camera>();
        float nodeScaleX = 0.3f;
        float nodeScaleY = 0.3f;


        for (int i = 0; i < trys; i++)
        {
            float x = Random.Range(-maximumDistance, maximumDistance);
            float y = Random.Range(-maximumDistance, maximumDistance);
            float r = x * x + y * y;
            if (r < minimumDistance || r > maximumDistance)
                continue;

            x += center.x;
            y += center.y;
            
            Vector3 p = c.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            if ((x < -p.x + nodeScaleX || x > p.x - nodeScaleX) || (y < -p.y + nodeScaleY || y > p.y - nodeScaleY))
            {
                continue;
            }
            
            bool breakFlag = false;
            for (int j = 0; j < NodeData.eventNodeList.Count; j++)
            {
                if (Vector3.Distance(NodeData.eventNodeList[j].transform.position, new Vector3(x, y, 0)) < minimumDistance)
                {
                    breakFlag = true;
                    break;
                }
            }
            
            if(breakFlag)
                continue;

            position.Set(x, y, 0);
            return true;
        }

        return false;
    }
}
