using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EventDialog : MonoBehaviour
{
    public string nodeName;
    private Color nameColor;
    public string nodeDescription;
    private Color descriptionColor;
    public UI_Description nameUI;
    public UI_Description descriptionUI;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // For testing
        UpdateName();
        UpdateDescription();
    }
    void UpdateName()
    {
        nameUI.Clear();
        nameUI.AddWords(nodeName);
        nameUI.UpdateSentence();
    }
    void UpdateDescription()
    {
        descriptionUI.Clear();
        descriptionUI.AddWords(nodeDescription);
        descriptionUI.UpdateSentence();
    }
    public void ChangeName(string _name, Color _color)
    {
        nodeName = _name;
        nameColor = _color;
        UpdateName();
    }
    public void ChangeDescription(string _description, Color _color)
    {
        nodeDescription = _description;
        descriptionColor = _color;
        UpdateDescription();
    }
}
