using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadByIndex(int sceneIndex)
    {
           SceneManager.LoadScene(sceneIndex);
    }
    public void LoadByIndexToStarFighter()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 0;
        SceneManager.LoadScene(3);
    }
    public void LoadByIndexToFlak()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 1;
        SceneManager.LoadScene(4);
    }
    public void LoadByIndexArmaments()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 2;
        SceneManager.LoadScene(5);
    }


    public void LoadBattleBridge(){
        SceneManager.LoadScene(1);
    }

    public void LoadNodeMapFromBattleBridge(){
        NodeData.nodeMap.SetActive(true);
        SceneManager.UnloadSceneAsync(2);
    }

    public void LoadAdditiveByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Additive);
    }
}
