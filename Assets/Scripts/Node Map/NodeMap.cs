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
        if (PlayerState.eventNodeList.Count == 0)
        {
            GenerateNodes();
            PlayerState.currentNode = PlayerState.eventNodeList[0];
            PlayerState.currentNode.GetComponent<EventNode>().Select();
        }
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
        PlayerState.eventNodeList.Add(node);
    }

    private bool placeNode(ref Vector3 position, Vector2 center)
    {
        // trys to place the node X amount of time before giving up
        int trys = 1000;
        //TODO: add screen bounds checks
        for (int i = 0; i < trys; i++)
        {
            float x = Random.Range(-maximumDistance, maximumDistance);
            float y = Random.Range(-maximumDistance, maximumDistance);
            float r = x * x + y * y;
            if (r < minimumDistance || r > maximumDistance)
                continue;

            x += center.x;
            y += center.y;
            
            bool breakFlag = false;
            for (int j = 0; j < PlayerState.eventNodeList.Count; j++)
            {
                if (Vector3.Distance(PlayerState.eventNodeList[j].transform.position, new Vector3(x, y, 0)) < minimumDistance)
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
