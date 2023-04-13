using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAgent : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float boardUnit;

    void Start()
    {
        cam.orthographicSize = boardUnit *1.3f / cam.aspect;
    }
}
