using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOS : MonoBehaviour
{
    private int rocksNeeded; // Total rocks needed for the SOS sign
    private int rocksPlaced = 0; // Track how many rocks have been placed
    // public Sprite[] sosStages; // Array of sprites for each stage of the SOS sign
    public bool isFinished = false;
    private SpriteRenderer[] spriteRenderers;

    void Start()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        rocksNeeded = spriteRenderers.Length;
    }

    public void AddRock()
    {
        if (rocksPlaced < rocksNeeded)
        {
            rocksPlaced++;
            UpdateSOS();
        }
    }

    private void UpdateSOS()
    {
        if (rocksPlaced <= rocksNeeded)
        {
            // GetComponent<SpriteRenderer>().sprite = sosStages[rocksPlaced]; // Update the sprite
            Color color = spriteRenderers[rocksPlaced-1].color;
            color.a = 1;
            spriteRenderers[rocksPlaced-1].color = color;
        }
        if (rocksPlaced == rocksNeeded)
        {
            isFinished = true;
        }
    }
}
