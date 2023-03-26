using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_IconBar : MonoBehaviour
{
    [SerializeField]
    int value = 10;
    public GameObject imageSizePrefab;
    public List<Sprite> icons;
    public float ratioValueToIcons = 1;
    public float horizontalStartOffset = 0f;
    public float horizontalOffset = 0.32f;
    public float verticalOffset = 0f;
    public bool isLeftToRight = true;
    public bool enableRemote = true;
    public UI_Description numberIndicator; // if empty, meaning do not show number
    public string indicatorFront;


    [Header("Effect Details")]
    public Color colorForAddValue = Color.white;
    public Color colorForDropValue = Color.red;
    public Color colorOfDescriptor = Color.white;
    public float addingTime = 1f;
    public float droppingTime = 1f;
    public bool hasAddingBumpEffect = false, hasDropingBumpEffect = false;
    public int addingBumpType = 0, droppingBumpType = 0;

    private List<GameObject> iconItems = new List<GameObject>();
    private UI_Description descriptor;


    // Start is called before the first frame update
    void Start()
    {
        descriptor = GetComponentInChildren<UI_Description>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIconBar();
    }

    void UpdateIconBar()
    {
        if (icons.Count == 0) return;

        // Updating value info
        if (value < 0) value = 0;
        if (descriptor)
        {
            descriptor.Clear();
            descriptor.AddWords(value.ToString(), colorOfDescriptor);
            descriptor.UpdateSentence();
        }

        // Updating Icons
        if (iconItems.Count < IconNumbers())
        {
            Debug.Log("Draw Icons");
            // Need to draw more icons.
            for (int i = iconItems.Count; i < IconNumbers(); ++i)
            {
                int imageIndex = i % icons.Count;
                if (!isLeftToRight)
                {
                    imageIndex = icons.Count - imageIndex - 1;
                }
                DrawIcon(i / icons.Count + 1, 1, imageIndex);
            }
        }
        else if (iconItems.Count > IconNumbers())
        {
            // Need to turn-off icons
            for (int i = iconItems.Count - 1; i >= IconNumbers(); --i)
            {
                VE_FadeOut tempVE = iconItems[i].AddComponent<VE_FadeOut>();
                tempVE.fadeoutTime = droppingTime;
                tempVE.destroyWhenFinished = true;
                tempVE.ChangeColor(colorForDropValue);
                VE_FadeIn conflictVE = tempVE.gameObject.GetComponent<VE_FadeIn>();
                if (conflictVE)
                {
                    conflictVE.StopVE();
                }
                iconItems.RemoveAt(i);
                if (hasDropingBumpEffect)
                {
                    VE_Bump tempBumpVE = tempVE.gameObject.AddComponent<VE_Bump>();
                    tempBumpVE.InitVE(0.32f, droppingTime, droppingBumpType);
                }
            }
        }

        // Update number indicator
        if (numberIndicator)
        {
            numberIndicator.Clear();
            numberIndicator.AddWords(indicatorFront + " " + value.ToString());
        }
    }
    public void ChangeBarValue(int _value)
    {
        if (enableRemote)
            value = _value;
    }
    int IconNumbers()
    {
        return (int)Mathf.Ceil((float)value / ratioValueToIcons);
    }

    void DrawIcon(int xCount, int yCount, int imageIndex)
    {
        float newX = (isLeftToRight ? this.transform.position.x + horizontalStartOffset + horizontalOffset * xCount : this.transform.position.x - horizontalOffset * xCount);
        float newY = this.transform.position.y;// + verticalOffset;

        Vector2 newPos = new Vector2(newX, newY);
        GameObject newIcon = Instantiate(imageSizePrefab, newPos, Quaternion.identity, this.transform);
        iconItems.Add(newIcon);
        Image _sr = newIcon.GetComponent<Image>();
        _sr.sprite = icons[imageIndex];
        VE_FadeIn tempVE = newIcon.AddComponent<VE_FadeIn>();
        tempVE.ChangeColor(colorForAddValue);
        tempVE.SetSolidFadein(true);
        tempVE.fadeinTime = addingTime;
        if (hasAddingBumpEffect)
        {
            VE_Bump tempBumpVE = tempVE.gameObject.AddComponent<VE_Bump>();
            tempBumpVE.InitVE(0.32f, addingTime, addingBumpType);
        }
    }
}
