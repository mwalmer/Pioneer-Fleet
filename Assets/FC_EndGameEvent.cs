using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class FC_EndGameEvent : MonoBehaviour
{
    public TextMeshProUGUI gameEndNotice;
    public CanvasGroup cGroup;
    public string nextScene;
    private float fadingTime = 2f;
    private float fadingCurrent = 0f;
    private float inputLockInterval = 1f;
    private float inputLockCurrent = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!gameEndNotice)
            gameEndNotice = this.GetComponentInChildren<TextMeshProUGUI>();
        if (!cGroup)
            cGroup = this.GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cGroup.alpha < 1)
        {
            fadingCurrent += Time.deltaTime;
            cGroup.alpha = Mathf.Lerp(cGroup.alpha, 1, fadingCurrent / fadingTime);
        }
        else
        {
            inputLockCurrent += Time.deltaTime;
            Time.timeScale = 0;
        }
    }
    public void GoNextScene()
    {
        Cursor.visible = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(nextScene);
    }

}
