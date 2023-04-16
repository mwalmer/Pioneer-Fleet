using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour
{
    public float cycleTime = 5f; // Time for one cycle through the rainbow spectrum
    private float hue = 0f; // Starting hue value
    private float saturation = 1f; // Full saturation
    private float brightness = 1f; // Full brightness
    private SpriteRenderer spriteRenderer; // Object's sprite renderer component

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the sprite renderer component on this object
    }

    void Update()
    {
        hue += Time.deltaTime / cycleTime; // Increment hue over time
        if (hue >= 1f) hue = 0f; // Wrap hue around if it goes over 1
        Color newColor = Color.HSVToRGB(hue, saturation, brightness); // Convert HSV values to RGB color
        spriteRenderer.color = newColor; // Set object's color to the new color
    }
}
