using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireLight : MonoBehaviour
{
    [SerializeField]
    private Light2D fireLight;
    [SerializeField]
    private Fire Fire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireLight.intensity = Fire.activityLevel*3f;
    }
}
