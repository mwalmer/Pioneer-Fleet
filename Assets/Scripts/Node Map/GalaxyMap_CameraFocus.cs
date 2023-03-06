using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMap_CameraFocus : MonoBehaviour
{
    public Transform focusedTarget = null;
    private Transform previousTarget = null;
    public Camera camera;
    public float focusSize = 2.7f;
    public float focusingTime = 2f;
    private float currentFocusingTime = 0f;
    private Vector3 originalPos;
    private int focusLock = 0; // -1 back, 0 idle, 1 focus
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!camera)
        {
            Debug.Log("CameraFocus Error: the function camera has not been set yet.");
        }

        if (focusedTarget)
        {
            if (focusedTarget != previousTarget)
            {
                focusLock = 1;
                currentFocusingTime = 0;
                previousTarget = focusedTarget;
            }
        }
        else
        {
            if (previousTarget != null)
            {
                focusLock = -1;
                currentFocusingTime = 0;
                previousTarget = null;
            }
        }

        if (focusLock == 1)
        {
            Focus();
        }
        else if (focusLock == -1)
        {
            Unfocus();
        }
        else
        {
            currentFocusingTime = 0;
        }
    }

    private void Focus()
    {
        currentFocusingTime += Time.deltaTime;

        // Scaling
        float timeRatio = currentFocusingTime / (focusingTime * 1.2f);
        float newSize = Mathf.Lerp(camera.orthographicSize, focusSize, timeRatio);
        camera.orthographicSize = newSize;

        // Tracking
        Vector3 newPos = new Vector3(focusedTarget.transform.position.x + (float)Screen.width / 450f / focusSize, focusedTarget.position.y, originalPos.z);
        newPos = Vector3.Lerp(transform.position, newPos, timeRatio);
        newPos.z = -10;
        camera.transform.position = newPos;

        // Lock Focus Function
        if (timeRatio >= 1) focusLock = 0;
    }
    private void Unfocus()
    {
        currentFocusingTime += Time.deltaTime;
        float timeRatio = currentFocusingTime / (focusingTime * 1.2f);
        float newSize = Mathf.Lerp(camera.orthographicSize, 5.4f, timeRatio);
        camera.orthographicSize = newSize;

        Vector3 newPos = Vector3.Lerp(transform.position, originalPos, timeRatio);
        newPos.z = -10;
        camera.transform.position = newPos;

        if (timeRatio >= 1) focusLock = 0;
    }
    public void SetFocus(Transform _target)
    {
        previousTarget = focusedTarget;
        focusedTarget = _target;
        currentFocusingTime = 0;
    }
    public void CancelFocus()
    {
        previousTarget = focusedTarget;
        focusedTarget = null;
        currentFocusingTime = 0;
    }
    public bool AbleChangeFocus()
    {
        return currentFocusingTime == 0;
    }
}
