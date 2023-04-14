using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_AutoOrdering : MonoBehaviour
{
    public SpriteRenderer orderingTarget;
    public int orderingOffset;
    private ParticleSystemRenderer particleRender;
    private ParticleSystem particle;
    public bool insistToPlay = true;
    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        particleRender = GetComponent<ParticleSystemRenderer>();
        if (insistToPlay) particle.Play();
    }

    // Update is called once per frame
    void Update()
    {
        particleRender.sortingLayerID = orderingTarget.sortingLayerID;
        particleRender.sortingOrder = orderingTarget.sortingOrder + orderingOffset;
        if (particle.isPlaying == false && insistToPlay)
        {
            particle.Play();
        }
    }
}
