using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPivot : MonoBehaviour
{
    public float rotationSpeed = 30f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.controls.Flashlight.PivotRight.WasPressedThisFrame())
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        if (InputManager.instance.controls.Flashlight.PivotLeft.WasPressedThisFrame())
        {
            transform.Rotate(0, 0, -rotationSpeed);
        }
    }
}
