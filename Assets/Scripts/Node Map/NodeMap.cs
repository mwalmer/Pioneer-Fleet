using TMPro;
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
        PlayerData playerData = FindObjectOfType<PlayerData>();
        GameObject.Find("UI_ResourceTab").GetComponent<UI_ResourceTab>().SetValue(playerData.currency);
    }

    private void OnDestroy()
    {
        if(NodeData.nodeMap != gameObject)
            NodeData.nodeMap.SetActive(true);
    }

    private void GenerateNodes()
    {
        NodeData.galaxyName = NodeData.GalaxyNames[Random.Range(0, NodeData.GalaxyNames.Count)];
        GameObject.Find("GalaxyNameDescriptor").GetComponent<TextMeshProUGUI>().SetText(NodeData.galaxyName);
        // Create/Place nodes
        Vector3 start = new Vector3(-4, 0, 0);
        Vector3 goal = new Vector3(4, 0, 0);
        Vector2 midpoint = new Vector2((goal.x + start.x) / 2, (goal.y + goal.y) / 2);
        
        CreateNode(start, "Start");
        CreateNode(goal, "Goal");
        
        for (int i = 0; i < numberOfNodes - 2; i++)
        {
            Vector3 position = new Vector3(0, 0, 0);
            if(!placeNode(ref position, midpoint, maximumDistance, minimumDistance))
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
            // re-rolls event if it picks the turncoat or boss, b/c those need to be unique
            EventData tempData = EventPresets.presets[Random.Range(0, EventPresets.presets.Count)];
            while(tempData.eventType == EventData.EventType.turncoat || tempData.eventType == EventData.EventType.boss)
                tempData = EventPresets.presets[Random.Range(0, EventPresets.presets.Count)];
            node.eventData = tempData;
        }

        EventData t = null;
        EventData b = null;
        for (int i = 0; i < EventPresets.presets.Count; i++)
        {
            if (EventPresets.presets[i].eventType == EventData.EventType.turncoat)
            {
                t = EventPresets.presets[i];
            }
            else if (EventPresets.presets[i].eventType == EventData.EventType.boss)
            {
                b = EventPresets.presets[i];
            }
        }
        
        if(t == null)
            Debug.LogError("why is their no turncoat preset?!");
        
        // choose random node set equal to turncoat preset
        // TODO: make sure this isn't the starting node! I think starting node is always 0
        int r = Random.Range(2, NodeData.eventNodeList.Count);
        //Debug.Log(r);
        GameObject turncoatNode = NodeData.eventNodeList[r];
        turncoatNode.GetComponent<EventNode>().eventData  = t;
        Color c = new Color(1.0f, .3f, .3f);
        turncoatNode.GetComponent<SpriteRenderer>().color = c;
        turncoatNode.name = "Turncoat";

        GameObject closestNode = null;
        float closest = 0;

        for (int i = 2; i < NodeData.eventNodeList.Count; i++)
        {
            GameObject n = NodeData.eventNodeList[i];
            float tempDist = Vector3.Distance(n.transform.position, turncoatNode.transform.position);
            if (closestNode == null || tempDist < closest)
            {
                if(n.GetComponent<EventNode>().eventData.eventType == EventData.EventType.turncoat)
                    continue;
                closestNode = n;
                closest = tempDist;
            }
        }
        
        if(closestNode == null)
            Debug.LogError("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        // add the boss next to turncoat node
        GameObject bossNode = closestNode;
        bossNode.GetComponent<EventNode>().eventData = b;
        bossNode.GetComponent<SpriteRenderer>().color = Color.red;
        bossNode.name = "Boss Node!";
        // NodeData.eventNodeList.Add(bossNode);
        
        for(int i = 0; i < NodeData.eventNodeList.Count; i++)
            NodeData.eventNodeList[i].GetComponent<EventNode>().LoadIcon();
        
        // if(!placeNode(ref bPos, new Vector2(turncoatNode.transform.position.x, turncoatNode.transform.position.y), maximumDistance / 2.0f, minimumDistance / 2.0f))
        //     Debug.Log("Failed to place!!!!!!!!!!!!!!!!!!!!!!");
        NodeData.eventNodeList.Remove(closestNode);
        NodeData.bossNode = bossNode;
        bossNode.SetActive(false);
    }

    private void CreateNode(Vector3 position, string str)
    {
        GameObject node = Instantiate(eventNodePrefab, position, new Quaternion(0, 0, 0, 0));
        node.transform.parent = gameObject.transform;
        node.name = "Event Node: " + str;
        NodeData.eventNodeList.Add(node);
    }

    private bool placeNode(ref Vector3 position, Vector2 center, float maxD, float minD)
    {
        // trys to place the node X amount of time before giving up
        int trys = 1000;
        //TODO: add screen bounds checks
        Camera c = GameObject.Find("Main Camera").GetComponent<Camera>();
        float nodeScaleX = 0.3f;
        float nodeScaleY = 0.3f;

        for (int i = 0; i < trys; i++)
        {
            float x = Random.Range(-maxD, maxD);
            float y = Random.Range(-maxD, maxD);
            float r = x * x + y * y;
            if (r < minD || r > maxD)
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
                if (Vector3.Distance(NodeData.eventNodeList[j].transform.position, new Vector3(x, y, 0)) < minD)
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
