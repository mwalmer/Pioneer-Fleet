using UnityEngine;

public class SpiralMovement : MonoBehaviour
{
    public float circleSpeed = 1f;
    public float forwardSpeed = -1f; // Assuming negative Z is towards the camera
    public float circleSize = 1f;
    public float circleGrowSpeed = 0.1f;

    private float currentCircleSize;
    private float currentZPos;

    private void Start()
    {
        currentCircleSize = circleSize;
        currentZPos = transform.position.z;
    }

    private void Update()
    {
        float xPos = Mathf.Sin(Time.time * circleSpeed) * currentCircleSize;
        float yPos = Mathf.Cos(Time.time * circleSpeed) * currentCircleSize;
        currentZPos += forwardSpeed * Time.deltaTime;

        transform.position = new Vector3(xPos, yPos, currentZPos);

        currentCircleSize += circleGrowSpeed;
    }
}