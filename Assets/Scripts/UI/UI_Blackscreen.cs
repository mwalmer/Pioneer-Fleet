using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blackscreen : MonoBehaviour
{
    /* Helper
        Before using, you need to setup the mainCanvas.
            example: UI_Blackscreen.mainCanvas = xxxx; 
            xxxx should be the canvas in your scene.
            this should only be set once.

        Please using "CallBlackscreen(numSlices, animeStatus)" to generate a sliced blackscreen.
        animeStatus => -1 close the blackscreen
                        1 open the blackscreen
                        0 idle // initially the blackscreen will be shown.
    
    */





    public static UI_Blackscreen blackscreen;
    public static Canvas mainCanvas;
    public List<RectTransform> slices;
    public int numSlices = 8;
    public float animeTime = 3f; // in sec
    [SerializeField]
    private int slicesStatus = 0; // -1 = close, 0 = idle, 1 = open
    private float currentTime = 0;

    private void Awake()
    {
        slices = new List<RectTransform>();


    }
    private void Update()
    {
        if (slicesStatus != 0)
        {
            currentTime += Time.deltaTime;
        }
        if (slicesStatus == -1)
        {
            //close the slices to take off the blackscreen
            CloseSlices();

        }
        else if (slicesStatus == 1)
        {
            //Open the slices to make a blackscreen
            OpenSlices();
        }


    }

    public static UI_Blackscreen CallBlackscreen(int _numSlices, int _beginAnimeStatus = 0, float _animeTime = 1.5f)
    {
        if (!mainCanvas)
        {
            Debug.Log("The main canvas have not been setup, call blackscreen is failed");
            return null;
        }
        if (!blackscreen)
        {
            GameObject container = new GameObject("Blackscreen");
            RectTransform rect = container.AddComponent<RectTransform>();
            rect.SetParent(mainCanvas.transform);
            container.AddComponent<CanvasRenderer>();

            rect.sizeDelta = new Vector2(Screen.width, Screen.height);
            rect.anchoredPosition = Vector2.zero;
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.localScale = new Vector3(1, 1, 1);

            blackscreen = container.AddComponent<UI_Blackscreen>();
            blackscreen.numSlices = _numSlices;
            blackscreen.animeTime = _animeTime;
            blackscreen.GenerateBlackSlices();
        }
        else
        {
            if (blackscreen.numSlices == _numSlices)
            {
                blackscreen.GenerateBlackSlices();
            }
        }

        if (_beginAnimeStatus == 1) blackscreen.OpenBlack();
        else if (_beginAnimeStatus == -1) blackscreen.CloseBlack();

        return blackscreen;
    }

    public void GenerateBlackSlices()
    {
        if (slices.Count > 0)
        {
            foreach (RectTransform slice in slices)
            {
                Destroy(slice.gameObject);
            }
            slices.Clear();
        }

        GameObject _slice = null;
        RectTransform _rect = null;
        Image _image = null;
        float width = Screen.width;
        float height = Screen.height / numSlices;
        for (int i = slices.Count; i < numSlices; ++i)
        {
            _slice = new GameObject();
            _slice.name = "Blackscreen_slice_" + (i + 1);
            _rect = _slice.AddComponent<RectTransform>();
            _rect.SetParent(blackscreen.transform);
            _slice.AddComponent<CanvasRenderer>();
            _image = _slice.AddComponent<Image>();
            _image.color = Color.black;
            _rect.localScale = new Vector3(1, 1, 1);
            _rect.anchorMax = new Vector2(0.5f, 1);
            _rect.anchorMin = new Vector2(0.5f, 1);
            _rect.localPosition = new Vector2(0, (height / -2) + (i * height) * -1);
            _rect.sizeDelta = new Vector2(width, height);

            slices.Add(_rect);
        }
    }
    public void OpenBlack()
    {
        if (slicesStatus == 0)
        {
            slicesStatus = 1;
            currentTime = 0;
        }
    }
    public void CloseBlack()
    {
        if (slicesStatus == 0)
        {
            slicesStatus = -1;
            currentTime = 0;
        }
    }
    private void OpenSlices()
    {
        float height = Screen.height / numSlices;
        float timeInterval = animeTime / (slices.Count);
        for (int i = 0, e = slices.Count - 1; i <= e; i++)
        {
            slices[i].sizeDelta = new Vector2(slices[i].sizeDelta.x, Mathf.Lerp(0, height, (currentTime - timeInterval * i) / (animeTime / numSlices * 2)));

            e = slices.Count - 1 - i;
            if (e != i)
            {
                slices[e].sizeDelta = new Vector2(slices[e].sizeDelta.x, Mathf.Lerp(0, height, (currentTime - timeInterval * i) / (animeTime / numSlices * 2)));
            }
        }
        if (currentTime > animeTime)
        {
            currentTime = 0;
            slicesStatus = 0;
        }
    }
    private void CloseSlices()
    {
        float height = Screen.height / numSlices;
        float timeInterval = animeTime / (slices.Count);
        for (int i = (slices.Count % 2 == 0 ? slices.Count / 2 : slices.Count / 2 + 1), e = 0, c = 0; i >= 0; i--, c++)
        {
            slices[i].sizeDelta = new Vector2(slices[i].sizeDelta.x, Mathf.Lerp(height, 0, (currentTime - timeInterval * c) / (animeTime / numSlices * 2)));

            e = slices.Count - 1 - i;
            if (e != i && e > i)
            {
                slices[e].sizeDelta = new Vector2(slices[e].sizeDelta.x, Mathf.Lerp(height, 0, (currentTime - timeInterval * c) / (animeTime / numSlices * 2)));
            }
        }
        if (currentTime > animeTime)
        {
            currentTime = 0;
            slicesStatus = 0;
        }
    }

    public Image GetImage(RectTransform _rect)
    {
        Image _image = _rect.gameObject.GetComponent<Image>();
        return (_image ? _image : null);
    }
    public bool IsFinished()
    {
        return slicesStatus == 0;
    }
}
