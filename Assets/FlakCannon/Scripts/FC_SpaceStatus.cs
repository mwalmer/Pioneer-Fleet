using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_SpaceStatus : MonoBehaviour
{
    [SerializeField]
    float distance = 0;
    public float sizeMultiplier = 1; // for changing default sprite size
    public SpriteRenderer sr;
    public float speed;
    public float distanceSpeed;
    public float planeSpeed;
    public Vector2 planeMoveVector;
    public float planeToDistanceModifier = 1000;


    public static float rangeOfVisibility = 2800;
    bool isDisabled = false;
    public bool isFlyOut = false;
    bool isUpsideDown = false;
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
        if (distance > rangeOfVisibility && isFlyOut)
        {
            Destroy(this.gameObject);
        }

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
    public void SetMoveAngular(float planeAngular, float distanceAngular)
    {
        planeAngular += 90; // offset the planeAngular to make the initial angle point to 0-clock.
        planeAngular = Mathf.Deg2Rad * planeAngular;
        distanceAngular = Mathf.Deg2Rad * distanceAngular;

        planeSpeed = Mathf.Sin(distanceAngular) * speed / planeToDistanceModifier;
        distanceSpeed = Mathf.Cos(distanceAngular) * speed;
        planeMoveVector = new Vector2(planeSpeed * Mathf.Cos(planeAngular), planeSpeed * Mathf.Sin(planeAngular));
    }
    public void Move(float _speed, float planeAngular, float distanceAngular, bool isFixedDelta = true)
    {
        float deltaTime = (isFixedDelta ? Time.fixedDeltaTime : Time.deltaTime);
        speed = _speed;
        SetMoveAngular(planeAngular, distanceAngular);

        if (isFlyOut)
        {
            distance = distance + distanceSpeed * deltaTime;
        }
        else
        {
            distance = distance - distanceSpeed * deltaTime;
        }

        this.transform.position = new Vector3(transform.position.x + planeMoveVector.x * deltaTime,
                                              transform.position.y + planeMoveVector.y * deltaTime,
                                              transform.position.z);
    }
    public void SetFlyOut(bool _isFlyOut)
    {
        isFlyOut = _isFlyOut;
    }
    public void SetUpsideDown(bool _isUpsideDown)
    {
        isUpsideDown = _isUpsideDown;
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

    public void AvatarRotationOn()
    {
        float degreeOfRotation = (planeSpeed * planeToDistanceModifier) / speed;
        degreeOfRotation = Mathf.Pow((degreeOfRotation > 1 ? 1 : degreeOfRotation), 2);

        float degreeOfSideMove = planeMoveVector.x / planeMoveVector.magnitude;
        float newZ = 0;
        if (isUpsideDown == false)
        {
            newZ = degreeOfSideMove * (90 * degreeOfRotation) * -1;
        }
        else
        {
            newZ = 180 + degreeOfSideMove * (90 * degreeOfRotation);
        }
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        newZ = (float)Mathf.RoundToInt(newZ * 1000) / 1000f;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, newZ);
    }
}
