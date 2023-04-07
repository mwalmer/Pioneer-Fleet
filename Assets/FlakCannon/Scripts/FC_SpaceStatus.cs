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
    bool isDisabled = false;
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

        if (distance > rangeOfVisibility || distance < 0 || transform.localScale.x <= 0.01f)
        {
            VisualDisable();
            //this.gameObject.SetActive(false);
        }
        else
        {
            if (isDisabled) return;
            VisualEnable();
        }
        sr.sortingOrder = (int)distance * -1;
        float modifier = (rangeOfVisibility - distance) / rangeOfVisibility * 4;
        modifier = modifier * modifier * sizeMultiplier;
        if (modifier < 0.01f)
            modifier = 0.01f;
        this.transform.localScale = new Vector3(modifier, modifier, transform.localScale.z);
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

    public void VisualDisable()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        sr.enabled = false;
        ParticleSystem ps = this.GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Stop();
            ps.Clear();
        }
    }
    public void VisualEnable()
    {
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 255);
        sr.enabled = true;
    }
    public void Disable()
    {
        isDisabled = true;
    }
}
