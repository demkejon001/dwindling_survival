using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float activityLevel = 1.0f;
    public ParticleSystem thisFire;
    public float decrementRate = 0.02f;
    public bool isLit = true;
    public float maxActivityLevel = 1.0f;
    public float firewoodIncrementValue = .15f;

    private void Start()
    {
        thisFire = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activityLevel > 0)
        {
            activityLevel -= Time.deltaTime * decrementRate;
            AdjustMainModule();
            isLit = true;
        }
        else
        {
            isLit = false;
            activityLevel = 0;
        }

    }

    void AdjustMainModule()
    {
        var main = thisFire.main;
        main.startSize = activityLevel;
    }

    public bool addFirewood()
    {
        if (activityLevel + firewoodIncrementValue < maxActivityLevel)
        {
            activityLevel += firewoodIncrementValue;
            return true;
        }
        return false;
    }
}
