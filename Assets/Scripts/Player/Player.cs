using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxWarmth = 100.0f;
    public float warmth = 100.0f;
    public float warmthIncrement = 1.0f;
    // Start is called before the first frame update

    public void WarmUp()
    {
        warmth = math.clamp(warmth + warmthIncrement, 0, maxWarmth);
    }
}
