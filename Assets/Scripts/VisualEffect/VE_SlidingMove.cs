using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VE_SlidingMove : MonoBehaviour
{
    [SerializeField]
    private int slidingStatus; // -1 = move to origin, 0 = idle, 1 = move to target
    public float slidingTime;
    public float slidingTarget = Screen.width / 100f;
    private RectTransform rectTransform;
    private Vector3 originalPos = Vector3.zero;
    private float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        if (!rectTransform)
        {
            rectTransform = this.gameObject.GetComponent<RectTransform>();
        }
        if (originalPos == Vector3.zero)
        {
            originalPos = rectTransform.position;
        }
        if (slidingTarget == 0)
            slidingTarget = Screen.width / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rectTransform) return; // This VE only works on UI components

        // The vector3 position has to be in rect transform format.
        if (slidingStatus != 0)
        {
            currentTime += Time.deltaTime;
            if (slidingStatus == 1)
            {
                float newX = Mathf.Lerp(rectTransform.position.x, slidingTarget, currentTime / slidingTime);
                if (Mathf.Abs(slidingTarget - newX) < 0.001f)
                {
                    newX = slidingTarget;
                    slidingStatus = 0;
                }
                rectTransform.position = new Vector3(newX, rectTransform.position.y, rectTransform.position.z);
            }
            else if (slidingStatus == -1)
            {
                float newX = Mathf.Lerp(rectTransform.position.x, originalPos.x, currentTime / slidingTime);
                if (Mathf.Abs(originalPos.x - newX) < 0.001f)
                {
                    newX = originalPos.x;
                    slidingStatus = 0;
                }
                rectTransform.position = new Vector3(newX, rectTransform.position.y, rectTransform.position.z);
            }
        }
    }

    public void InitVE(float _slidingDistance, float _slidingTime)
    {
        if (!rectTransform)
        {
            originalPos = rectTransform.position;
        }
        slidingTarget = _slidingDistance;
        slidingTime = _slidingTime;
        currentTime = 0;
    }
    public void MoveToTarget(float _slidingDistance)
    {
        slidingTarget = _slidingDistance;
        slidingStatus = 1;
    }
    public void MoveToTarget()
    {
        slidingStatus = 1;
    }
    public void MoveToOrigin()
    {
        slidingStatus = -1;
    }
    public void Pause()
    {
        slidingStatus = 0;
    }
    public void Reset()
    {
        if (rectTransform)
        {
            rectTransform.position = new Vector3(originalPos.x, originalPos.y, originalPos.z);

            return;
        }
        Debug.Log(gameObject.name + "has no rect transform, VE_slidingMove is not working.");
    }
    public void Reset(Vector3 _newOrignPos)
    {
        if (rectTransform)
        {
            originalPos = _newOrignPos;
            Reset();
            return;
        }
        Debug.Log(gameObject.name + "has no rect transform, VE_slidingMove is not working.");
    }
    public void Run()
    {

    }
}
