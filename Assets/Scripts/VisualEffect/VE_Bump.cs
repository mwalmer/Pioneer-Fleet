using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VE_Bump : MonoBehaviour
{
    public float maxHeight = 1f; // referencing with the original position.
    public int bumpType = 0; // -1 = only fall, 0 = normal, 1 = only raise
    public float effectTime; // in sec
    private float currentHeight = 0;
    private float currentTime;
    private float originY;
    // Start is called before the first frame update
    void Start()
    {
        originY = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > effectTime)
        {
            this.gameObject.transform.position = new Vector3(transform.position.x, originY, transform.position.z);
            StopVE();
            return;
        }

        if (Mathf.Abs(bumpType) == 1)
        {
            // only raise or fall
            currentHeight += maxHeight * (Time.deltaTime / effectTime) * bumpType;
        }
        else
        {
            // normal
            if (currentTime < effectTime / 2)
            {
                currentHeight += (maxHeight * 2) * (Time.deltaTime / effectTime);
            }
            else
            {
                currentHeight -= (maxHeight * 2) * (Time.deltaTime / effectTime);
            }
        }
        if (currentHeight < 0) currentHeight = 0;
        else if (bumpType == -1 && currentHeight > maxHeight) currentHeight = maxHeight;
        float newY = originY + currentHeight;
        this.gameObject.transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void InitVE(float _height, float _effectTime, int _bumpType)
    {
        maxHeight = _height;
        effectTime = _effectTime;
        bumpType = _bumpType;
        if (_bumpType == -1) currentHeight = _height;
    }
    public void StopVE()
    {
        Destroy(this);
    }

}
