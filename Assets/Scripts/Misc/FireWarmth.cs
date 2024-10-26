using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public class FireWarmth : MonoBehaviour
{
    private Fire fire;
    void Start()
    {
        fire = GetComponentInParent<Fire>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (fire.isLit)
        {
            if (collision.CompareTag("Player"))
            {
                Player player = collision.GetComponent<Player>();
                player.isInFireRadius = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            player.isInFireRadius = false;
        }
    }

}
