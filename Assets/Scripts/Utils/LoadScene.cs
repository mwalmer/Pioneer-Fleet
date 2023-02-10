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

    public void LoadBattleScene(){
        //GameObject.Find("StageData").GetComponent<StageData>().setStage();
        SceneManager.LoadScene(1);
    }
}
