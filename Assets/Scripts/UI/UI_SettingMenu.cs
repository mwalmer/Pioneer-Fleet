using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_SettingMenu : MonoBehaviour
{
    public string titleScene;

    public float fadingTime = 2f;
    [SerializeField]
    private CanvasGroup cGroup = null;
    private int fadingStatus = 0; // -1 fade in, 0 = idle, 1 fade out
    private float currentFadingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cGroup = gameObject.GetComponent<CanvasGroup>();
        if (!cGroup) cGroup = gameObject.AddComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

        // On Off Menu Animation
        if (fadingStatus == 1)
        {
            FadeOut();
        }
        else if (fadingStatus == -1)
        {
            FadeIn();
        }
        else
        {

        }


    }

    void FadeIn()
    {
        currentFadingTime += Time.deltaTime;
        cGroup.alpha = Mathf.Lerp(cGroup.alpha, 1, currentFadingTime / (fadingTime * 10f));

        if (cGroup.alpha >= 0.9999f)
        {
            cGroup.alpha = 1;
            fadingStatus = 0;
            PauseGame();
        }
    }
    void FadeOut()
    {
        currentFadingTime += Time.deltaTime;
        cGroup.alpha = Mathf.Lerp(cGroup.alpha, 0, currentFadingTime / (fadingTime * 10f));

        if (cGroup.alpha <= 0.0001f)
        {
            cGroup.alpha = 0;
            fadingStatus = 0;
            ResumeGame();
            // It has to be set to false. If doesn't, it will block all other buttons.
            gameObject.SetActive(false);
        }
    }

    public void OnOffMenu()
    {
        if (cGroup.alpha != 0 && cGroup.alpha != 1) return;
        if (this.gameObject.activeInHierarchy == false)
        {
            gameObject.SetActive(true);
            cGroup.alpha = 0;
            fadingStatus = -1;
            currentFadingTime = 0;
        }
        else
        {
            // Do fade out animation and set to false
            ResumeGame();
            fadingStatus = 1;
            currentFadingTime = 0;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void BackToTitle()
    {
        SceneManager.LoadScene(titleScene);
    }
    public void SurroundInMinigame()
    {

    }
}
