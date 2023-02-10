using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_SpaceStatus : MonoBehaviour
{
    [SerializeField]
    float distance = 0;
    public float sizeMultiplier = 1; // for changing default sprite size
    public SpriteRenderer sr;

    public static float rangeOfVisibility = 2800;
    // Start is called before the first frame update
    void Start()
    {
        if (!sr)
        {
            sr = GetComponentInChildren<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        VisualScaling();
    }
    void FixedUpdate()
    {

    }

    void VisualScaling()
    {
        if (!sr)
            return;

        // Use to change the scale of the enemy fighter by the distance

        if (distance > rangeOfVisibility || distance < 0)
        {
            sr.enabled = false;
            this.gameObject.SetActive(false);
        }
        else
            sr.enabled = true;
        sr.sortingOrder = (int)distance;
        float Modifier = (rangeOfVisibility - distance) / rangeOfVisibility * 4;
        Modifier = Modifier * Modifier * sizeMultiplier;

        this.transform.localScale = new Vector3(Modifier, Modifier, transform.localScale.z);
    }
    public void SetSR(SpriteRenderer _sr)
    {
        sr = _sr;
    }
    public void RandomInitDistance(float lower, float upper)
    {
        if (lower >= upper)
        {
            distance = lower;
            return;
        }
        distance = lower + Random.Range(0, upper - lower);
    }
    public void Move(float speedPerSec, bool toFar = false, bool isFixedDelta = true)
    {
        if (!toFar)
            distance = distance - speedPerSec * (isFixedDelta ? Time.fixedDeltaTime : 1);
        else
            distance = distance + speedPerSec * (isFixedDelta ? Time.fixedDeltaTime : 1);
    }


    public float GetDistance()
    {
        return distance;
    }

    
}
