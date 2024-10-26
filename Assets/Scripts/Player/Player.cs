using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxWarmth = 100.0f;
    public float warmth = 100.0f;
    public float warmthIncrement = 1.0f;
    public int[] inventory = new int[Enum.GetValues(typeof(CollectableObject)).Length];
    public TextMeshProUGUI num;

    public void WarmUp()
    {
        warmth = math.clamp(warmth + warmthIncrement, 0, maxWarmth);
    }

    public void AddObject(CollectableObject objectToAdd)
    {
        int index = (int)objectToAdd.id;
        inventory[index] += 1;
        num.text = inventory[index].ToString();
    }

    private void Start()
    {
        Array.Fill(inventory, 0);
    }
}
