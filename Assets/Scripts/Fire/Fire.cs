using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float activityLevel = 1.63f;
    public ParticleSystem thisFire;
    public float decrementRate = 0.1f;
    public bool isLit = true;

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
        if (activityLevel < 0) 
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
}
