using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMoveForward : MonoBehaviour
{
    public float frequencyTime = 0.5f;
    public Vector2 speed;
    public bool moveCountClockwise = true;
    private float currentTime = 0;
    private float currentAngle = 0;
    public float yAmplitude = 1f;
    public float xAmplitude = 1;
    
    private Vector3 center;

    // Start is called before the first frame update
    void Start()
    { Init();

    }
   void Init()
    {
        center = this.transform.position;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;

        center = new Vector3(center.x + speed.x * Time.fixedDeltaTime, center.y + speed.y * Time.fixedDeltaTime, center.z);

        currentAngle = currentTime / frequencyTime * 360 * Mathf.Deg2Rad * (moveCountClockwise ? -1 : 1);

        float newX = Mathf.Cos(currentAngle) * xAmplitude;
        float newY = Mathf.Sin(currentAngle) * yAmplitude;

        transform.position = new Vector3(center.x + newX, center.y + newY, center.z);

        if (currentTime >= frequencyTime)
        {
            currentTime -= frequencyTime;
        }
    }

    public void SetSpeed(float xSpeed, float ySpeed)
    {
        speed = new Vector2(xSpeed, ySpeed);
    }

}
