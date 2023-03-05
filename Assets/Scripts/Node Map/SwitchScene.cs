using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadScene(int id)
    {
        // disable NodeMap stuff
        if(!NodeData.nodeMap)
            NodeData.nodeMap = GameObject.Find("NodeMap");
        NodeData.nodeMap.SetActive(false);
        
        // load scene additively
        SceneManager.LoadScene(id, LoadSceneMode.Additive);
    }
}
