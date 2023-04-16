using UnityEngine;

public class SineWave : MonoBehaviour
{
    public float frequency = 1f; // frequency of the sine wave
    public float speed = 1f; // speed at which the object travels along the sine wave
    public float amplitude = 1f; // maximum amplitude of the sine wave
    public float distanceThreshold = 1f; // maximum distance from the center for the sine wave to oscillate

    private float xPosition; // current x position of the object
    private float yPosition; // current y position of the object
    private float distanceFromCenter; // distance from the center of the sine wave

    private void Start()
    {
        // Set the initial x position of the object
        xPosition = transform.position.x;

        // Calculate the distance from the center of the sine wave
        distanceFromCenter = Mathf.Abs(transform.position.z);

        // Calculate the amplitude of the sine wave based on the distance from the center
        amplitude = Mathf.Lerp(0, amplitude, 1 - (distanceFromCenter / distanceThreshold));
    }

    private void Update()
    {
        // Update the x position based on the speed
        xPosition += Time.deltaTime * speed;

        // Calculate the y position using a sine wave function
        yPosition = amplitude * Mathf.Sin(2 * Mathf.PI * frequency * xPosition);

        // Update the position of the object
        transform.position = new Vector3(xPosition, transform.position.y, transform.position.z + yPosition);
    }
}
