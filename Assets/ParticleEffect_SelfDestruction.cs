using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect_SelfDestruction : MonoBehaviour
{
    ParticleSystem particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particleSystem && particleSystem.isPlaying == false)
        {
            Destroy(this.gameObject);
        }
    }
}
