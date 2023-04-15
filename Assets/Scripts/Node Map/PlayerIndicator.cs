using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
    public float speed;
    public bool travel = false;
    private GameObject currentNode;
    private Vector3 destination;
    private Vector3 direction;
    
    private void Start()
    {
        currentNode = NodeData.currentNode;
        SetOnPlanet();
    }

    private void Update()
    {
        if (currentNode != NodeData.currentNode)
        {
            travel = true;
            UpdateDestination();
            currentNode = NodeData.currentNode;
            // SetOnPlanet();
        }

        if (travel)
        {
            if (Vector3.Distance(currentNode.transform.position, transform.position) < 0.45f)
                travel = false;
                
            transform.position += direction * .5f * Time.deltaTime;
            return;
        }

        CirclePlanet();
    }

    private void CirclePlanet()
    {
        transform.RotateAround(currentNode.transform.position, new Vector3(0, 0, 1), speed * Time.deltaTime);
    }

    private void SetOnPlanet()
    {
        Vector3 vec = currentNode.transform.position;
        vec.x += .45f;
        transform.position = vec;
        transform.rotation = Quaternion.identity;
        Debug.Log("Running");
    }

    private void UpdateDestination()
    {
        Vector3 offset = currentNode.transform.position - transform.position;
        destination = NodeData.currentNode.transform.position - offset;
        direction = destination - transform.position;
        direction = Vector3.Normalize(direction);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
