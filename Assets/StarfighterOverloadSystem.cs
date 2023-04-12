using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterOverloadSystem : MonoBehaviour
{   public ParticleSystem particleBody;
    
    // Start is called before the first frame update
    void Start()
    {
        particleBody.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OverloadOn()
    {
        particleBody.Play();
    }
    public void OverloadOff()
    {
         particleBody.Stop();
    }
}
