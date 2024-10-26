using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float activityLevel = 1.63f;
    public ParticleSystem thisFire;
    public float decrementRate = 0.1f;

    private void Start()
    {
        thisFire = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        activityLevel -= Time.deltaTime * decrementRate;
        AdjustMainModule();
    }

    void AdjustMainModule()
    {
        var main = thisFire.main;
        main.startSize = activityLevel;
    }
}
