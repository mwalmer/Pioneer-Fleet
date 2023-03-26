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
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 1;
        GameObject.Find("PlayerData").GetComponent<PlayerData>().offScreen();
        SceneManager.LoadScene(3);
    }
    public void LoadByIndexToFlak()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 2;
        GameObject.Find("PlayerData").GetComponent<PlayerData>().offScreen();
        SceneManager.LoadScene(4);
    }
    public void LoadByIndexArmaments()
    {
        GameObject.Find("PlayerData").GetComponent<PlayerData>().miniGame = 3;
        GameObject.Find("PlayerData").GetComponent<PlayerData>().offScreen();
        SceneManager.LoadScene(5);
    }


    public void LoadBattleBridge(){
        SceneManager.LoadScene(2);
    }

    public void LoadBattleBridgeFromMiniGame(){

        SceneManager.LoadScene(2);
    }

    public void LoadNodeMapFromBattleBridge(){
        GameObject.Find("PlayerData").GetComponent<PlayerData>().offScreen();
        SceneManager.LoadScene(1);
    }

    public void LoadAdditiveByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex,LoadSceneMode.Additive);
    }
}
