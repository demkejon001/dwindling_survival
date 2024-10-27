using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPivot : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public KeyCode upKeyCode = KeyCode.KeypadEnter;
    public KeyCode downKeyCode = KeyCode.Keypad0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(upKeyCode))
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        else if (Input.GetKeyDown(downKeyCode))
        {
            transform.Rotate(0, 0, -rotationSpeed );
        }
    }
}
