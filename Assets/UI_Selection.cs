using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_Selection : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Button currentSelection;
    public List<Button> selections;
    public float spacingInterval = 0.4f; // comparing to the button size;
    private RectTransform container;

    public int testCount = 3;

    // Start is called before the first frame update
    void Start()
    {
        selections = new List<Button>();
        container = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Test code
        if (selections.Count < testCount)
        {
            for (int i = selections.Count; i < testCount; ++i)
            {
                RegisterSelection("SelectionButton_" + i, "A auto created button_" + i);
            }
        }

        DetectPlayerSelection();
    }
    public void CleanSelections()
    {
        // changed from foreach to for, deleting things in foreach caused errors
        for(int i = 0; i < selections.Count; i++)
        {
            Button temp = selections[i];
            selections.Remove(temp);
            Destroy(temp);
        }
    }

    public Button RegisterSelection(string selectionName, string text, string selectionType = "None")
    {
        Button newSelection = Instantiate(buttonPrefab, this.transform).GetComponent<Button>();
        RectTransform tRect = newSelection.GetComponent<RectTransform>();
        tRect.anchorMax = Vector2.one;
        tRect.anchorMin = Vector2.zero;
        tRect.offsetMax = new Vector2(-16, 0);
        tRect.offsetMin = new Vector2(16, 167);
        newSelection.gameObject.name = selectionName;
        newSelection.GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (selectionType != "None")
        {
            //TODO:: Will show a icon for player predict the result of the event selection


        }

        selections.Add(newSelection);
        RearrangeSelections();

        return newSelection;
    }

    public string GetCurrentSelection()
    {
        if (!currentSelection) return null;
        return currentSelection.gameObject.name;
    }

    private void RearrangeSelections()
    {
        float selectionHeight = container.rect.height / (selections.Count + (selections.Count - 1) * spacingInterval);
        float selectionInterval = selectionHeight * spacingInterval;
        Debug.Log(container.rect.height + " | " + selectionHeight);
        RectTransform tRect;
        for (int i = 0; i < selections.Count; ++i)
        {
            Debug.Log("Modifying " + selections[i].gameObject.name);
            tRect = selections[i].GetComponent<RectTransform>();
            tRect.offsetMin = new Vector2(tRect.offsetMin.x, (selections.Count - i - 1) * (selectionHeight + selectionInterval));
            tRect.offsetMax = new Vector2(tRect.offsetMax.x, i * (selectionHeight + selectionInterval) * -1);
        }
    }

    public void DetectPlayerSelection()
    {
        foreach (Button selection in selections)
        {
            if (EventSystem.current.currentSelectedGameObject == selection.gameObject)
            {
                currentSelection = selection;
                return;
            }
        }
        currentSelection = null;
    }
}
