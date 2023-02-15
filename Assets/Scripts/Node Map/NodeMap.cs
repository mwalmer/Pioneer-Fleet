using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    public int numberOfNodes;
    public GameObject eventNodePrefab;
    public float minimumDistance;
    public float maximumDistance;

    private void Awake()
    {
        if (NodeData.eventNodeList.Count == 0)
        {
            GenerateNodes();
            NodeData.currentNode = NodeData.eventNodeList[0];
            NodeData.currentNode.GetComponent<EventNode>().Select();
        }
        
        // Debug.Log(Screen.width);
        // Debug.Log(Screen.height);
    }

    private void GenerateNodes()
    {
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
    }

    private void CreateNode(Vector3 position, string str)
    {
        GameObject node = Instantiate(eventNodePrefab, position, new Quaternion(0, 0, 0, 0));
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
            Debug.Log(p);
            if ((x < -p.x + nodeScaleX || x > p.x - nodeScaleX) || (y < -p.y + nodeScaleY || y > p.y - nodeScaleY))
            {
                Debug.Log(nodeScaleX);
                Debug.Log(nodeScaleY);
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
                
                // if(Screen.)
            }
            
            if(breakFlag)
                continue;

            position.Set(x, y, 0);
            return true;
        }

        return false;
    }
}
