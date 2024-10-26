using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool isInFireRadius = false;
    public float maxWarmth = 100.0f;
    public float warmth = 100.0f;
    public float warmthIncrementRate = 1.0f;
    public float warmthDecrementRate = 1.0f;
    public int[] inventory = new int[Enum.GetValues(typeof(ItemID)).Length];

    private void Start()
    {
        Array.Fill(inventory, 0);
    }
    public void WarmUp()
    {
    }

    public void AddObject(CollectableObject objectToAdd)
    {
        int index = (int)objectToAdd.id;
        inventory[index] += 1;
        GameManager.Instance.UpdateInventoryUI(index);
    }

    void Update()
    {
        if (isInFireRadius)
        {
            warmth = Mathf.Min(warmth + Time.deltaTime * warmthIncrementRate, maxWarmth);
        }
        else
        {
            warmth = Mathf.Max(warmth - Time.deltaTime * warmthDecrementRate, 0);
        }
    }
}
