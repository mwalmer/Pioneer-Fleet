using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_AutoOrdering : MonoBehaviour
{
    public SpriteRenderer orderingTarget;
    private ParticleSystemRenderer particleRender;
    // Start is called before the first frame update
    void Start()
    {
        particleRender = GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        particleRender.sortingLayerID = orderingTarget.sortingLayerID;
        particleRender.sortingOrder = orderingTarget.sortingOrder - 1;
    }
}
