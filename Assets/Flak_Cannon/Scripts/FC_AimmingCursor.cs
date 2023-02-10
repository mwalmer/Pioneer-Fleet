using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_AimmingCursor : MonoBehaviour
{
    public bool disableOriginalCursor = true;

    private void Start()
    {
        Cursor.visible = !disableOriginalCursor;
    }
    // Update is called once per frame
    void Update()
    {
        if (disableOriginalCursor)
            Cursor.visible = false;
        else
            Cursor.visible = true;


        TrackMouse();
    }

    void TrackMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        this.transform.position = new Vector3(worldPos.x, worldPos.y, mousePos.z);
    }
}
