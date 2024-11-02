using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // public TextMeshProUGUI description, info, action;
    public Player player;
    public float interactionRadius = 1.0f;

    void Start()
    {
        if (player == null)
        {
            player = GetComponentInParent<Player>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
        
    }

    private void Interact()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.transform.position, interactionRadius);
        foreach (Collider2D collider in colliders)
        {
            CollectableObject collectableObject = collider.GetComponent<CollectableObject>();
            Fire fire = collider.GetComponentInParent<Fire>();
            SOS sos = collider.GetComponent<SOS>();

            if (collectableObject != null)
            {
                if (collectableObject.id == ItemID.Berry)
                {
                    player.Eat(10.0f);
                }
                else
                {
                    player.AddObject(collectableObject);
                }
                Destroy(collectableObject.gameObject);
            }
            if (fire != null)
            {
                if (player.inventory[(int) ItemID.Wood] > 0)
                {
                    bool addedFirewood = fire.addFirewood();
                    if (addedFirewood)
                    {
                        player.RemoveObjectFromInventory(ItemID.Wood);
                    }
                }
            }
            if (sos != null)
            {
                if (player.inventory[(int) ItemID.Rock] > 0)
                {
                    sos.AddRock();
                    player.RemoveObjectFromInventory(ItemID.Rock);
                }
            }
        }
    }
}
