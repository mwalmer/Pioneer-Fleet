using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfighterOverloadSystem : MonoBehaviour
{
    [Header("Wing Thrusters")]
    public ParticleSystem leftWing;
    public ParticleSystem rightWing;
    public ParticleSystem mainThruster;

    [Header("Steam Effect")]
    public ParticleSystem leftSteam;
    public ParticleSystem rightSteam;

    [Header("BodyResidual")]
    public ParticleSystem residual;
    public ParticleSystem bodyLights;

    [Header("QuantumParticles")]
    public List<ParticleSystem> quantums;
    private List<ParticleSystem> notQuantums;

    private bool isActived;
    private bool isFinishedOnAction;
    private float currentTime = 0;
    private float residualDelayTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        notQuantums = new List<ParticleSystem>();
        notQuantums.Add(leftWing);
        notQuantums.Add(rightWing);
        notQuantums.Add(mainThruster);
        notQuantums.Add(residual);
        notQuantums.Add(bodyLights);

        SetDefault();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActived && isFinishedOnAction == false)
        {
            if (leftSteam.isPlaying == false || rightSteam.isPlaying == false)
            {
                currentTime += Time.deltaTime;
                ThrustersOn();
                QuantumsOn();

                if (currentTime >= residualDelayTime)
                {
                    BodyResidualOn();
                    isFinishedOnAction = true;
                }
            }
        }
    }

    public void OverloadOn()
    {
        leftSteam.Play();
        rightSteam.Play();
        isActived = true;
    }
    public void OverloadOff()
    {
        SetDefault();
    }
    void QuantumsOn()
    {
        foreach (ParticleSystem quantum in quantums)
        {
            if (quantum.isPlaying == false) quantum.Play();
        }
    }
    void ThrustersOn()
    {
        leftWing.startColor = Color.red;
        rightWing.startColor = Color.red;
        mainThruster.startColor = Color.red;
    }
    void ThrusterOff()
    {
        leftWing.startColor = Color.white;
        rightWing.startColor = Color.white;
        mainThruster.startColor = Color.white;
    }
    void BodyResidualOn()
    {
        bodyLights.Play();
        residual.Play();
    }

    void SetDefault()
    {
        isActived = false;
        isFinishedOnAction = false;
        currentTime = 0;
        foreach (ParticleSystem quantum in quantums)
        {
            quantum.Stop();
        }
        foreach (ParticleSystem notQuantum in notQuantums)
        {
            notQuantum.Stop();
        }

        ThrusterOff();
    }



}
