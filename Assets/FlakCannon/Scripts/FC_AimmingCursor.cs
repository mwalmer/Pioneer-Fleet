using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FC_AimmingCursor : MonoBehaviour
{
    public static bool disableOriginalCursor = true;
    private void Start()
    {
        Cursor.visible = !disableOriginalCursor;
    }
    // Update is called once per frame
    void Update()
    {
        if (FC_GameManager.IsGameActive == false) return;

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

    public Vector2 GetPos()
    {
        return transform.position;
    }
}
