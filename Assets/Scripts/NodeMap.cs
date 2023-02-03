using System.Collections.Generic;
using UnityEngine;

public class NodeMap : MonoBehaviour
{
    public int numberOfNodes;
    public GameObject eventNodePrefab;
    public float minimumDistance;
    public float maximumDistance;
    
    // save it between scenes, shouldn't change each time scene is loaded
    private List<GameObject> _eventNodeList;
    
    private void Awake()
    {
        _eventNodeList = new List<GameObject>();
        GenerateNodes();
    }

    private void Start()
    {
        //TODO: change how this is selected
        PlayerState.currentNode = _eventNodeList[0];
        PlayerState.currentNode.GetComponent<EventNode>().Select();
    }

    private void GenerateNodes()
    {
        Vector3 start = new Vector3(-4, 0, 1);
        Vector3 goal = new Vector3(4, 0, 1);
        Vector2 midpoint = new Vector2((goal.x + start.x) / 2, (goal.y + goal.y) / 2);
        
        GameObject node = Instantiate(eventNodePrefab, start, new Quaternion(0, 0, 0, 0));
        node.name = "Event Node start";
        _eventNodeList.Add(node);


        //NOTE: should probably be moved to awake
        for (int i = 0; i < numberOfNodes - 2; i++)
        {
            Vector3 position = new Vector3(0, 0, 1);
            if(!placeNode(ref position, midpoint))
                continue;
            node = Instantiate(eventNodePrefab, position, new Quaternion(0, 0, 0, 0));
            node.name = "Event Node " + i;
            _eventNodeList.Add(node);
        }
        
        node = Instantiate(eventNodePrefab, goal, new Quaternion(0, 0, 0, 0));
        node.name = "Event Node goal";
        _eventNodeList.Add(node);
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
            for (int j = 0; j < _eventNodeList.Count; j++)
            {
                if (Vector3.Distance(_eventNodeList[j].transform.position, new Vector3(x, y, 1)) < minimumDistance)
                {
                    breakFlag = true;
                    break;
                }
            }
            
            if(breakFlag)
                continue;

            position.Set(x, y, 1.0f);
            return true;
        }

        return false;
    }
}
