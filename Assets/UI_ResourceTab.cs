using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ResourceTab : MonoBehaviour
{
    public TextMeshProUGUI additionText;
    public UI_DynamicNumber number;
    public bool playAdditionAnimation = true;
    private Queue<int> additionQueue;
    private float animationTime = 0.5f;
    private float currentTime = -1;

    [Space]
    public bool testButton = false;

    // Start is called before the first frame update
    void Start()
    {
        additionQueue = new Queue<int>();
        number.dynamicTime = 0.33f;
    }

    // Update is called once per frame
    void Update()
    {
        if (testButton)
        {
            testButton = false;
            AddAmount(100);
        }



        if (additionQueue.Count > 0)
        {
            if (currentTime < 0)
            {
                currentTime = 0;
            }

            currentTime += Time.deltaTime;

            float newY = Mathf.Lerp(64, 0, currentTime / animationTime);
            additionText.rectTransform.anchoredPosition = new Vector2(additionText.rectTransform.anchoredPosition.x, newY);
            additionText.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, currentTime / animationTime);
            int amount = additionQueue.Peek();
            additionText.text = (amount >= 0 ? "+" : "") + amount;

            if (currentTime >= animationTime)
            {
                currentTime = -1;
                number.AddValue(additionQueue.Dequeue());
            }
        }
    }

    public void AddAmount(int amount)
    {
        additionQueue.Enqueue(amount);
    }
    public void SetValue(int value)
    {
        number.SetValue(value);
    }
}
