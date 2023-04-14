using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FC_CannonFireEffector : MonoBehaviour
{
    public float recoilTime = 0.5f;
    float leftCurrentTime = -1;
    float rightCurrentTime = -1;
    public bool leftBarrelFired = false;
    public bool rightBarrelFired = false;
    public Animator cannonAnimator;
    public ParticleSystem leftCannonFire;
    public ParticleSystem rightCannonFire;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cannonAnimator)
        {
            Debug.Log("FireAnimation!");
            cannonAnimator.SetBool("LeftBarrelFired", leftBarrelFired);
            cannonAnimator.SetBool("RightBarrelFired", rightBarrelFired);
            Debug.Log(cannonAnimator.GetBool("RightBarrelFired"));
        }

    }
    public void FireLeftEffect()
    {
        leftCannonFire.Play();
        cannonAnimator.SetBool("LeftBarrelFired", true);
        leftBarrelFired = true;
    }
    public void FireRightEffect()
    {
        rightCannonFire.Play();
        cannonAnimator.SetBool("RightBarrelFired", true);
        rightBarrelFired = true;
    }
}
