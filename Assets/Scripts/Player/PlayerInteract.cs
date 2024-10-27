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
    public Dictionary<int, CollectableObject> collectables = new Dictionary<int, CollectableObject>();
    public Dictionary<int, InteractableObject> interacables = new Dictionary<int, InteractableObject>();
    public bool nextToSOS;
    private SOS sosSign;
    public bool nextToFire;
    private Fire fire;

    void Start()
    {
        // description.text = "";
        // info.text = "";
        // action.text = "";
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
        if (collectables.Count > 0)
        {
            List<int> remove_keys = new List<int>();
            foreach (KeyValuePair<int, CollectableObject> kvp in collectables)
            {
                if (kvp.Value != null)
                {
                    player.AddObject(kvp.Value);
                    remove_keys.Add(kvp.Key);
                }
                else
                {
                    remove_keys.Add(kvp.Key);
                }
            }

            foreach (int key in remove_keys)
            {
                CollectableObject collectableObject = collectables[key];
                collectables.Remove(key);
                if (collectableObject != null)
                {
                    Destroy(collectableObject.gameObject);
                }
            }
        }

        if (nextToSOS)
        {
            if (player.inventory[(int) ItemID.Rock] > 0)
            {
                sosSign.AddRock();
                player.RemoveObjectFromInventory(ItemID.Rock);
            }
        }
        if (nextToFire)
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // InteractableObject objectDetected = collision.gameObject.GetComponent<InteractableObject>();
        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();
        SOS sos = collision.gameObject.GetComponent<SOS>();
        Fire fire_ = collision.gameObject.GetComponentInParent<Fire>();

        // if (objectDetected != null)
        // {
        //     description.text = objectDetected.typeOfInteractable;
        //     info.text = objectDetected.interactableInfo;
        //     action.text = objectDetected.actionToDo;
        // }
        if (collectableDetected != null) 
        {
            int hash_code = collectableDetected.gameObject.GetHashCode();
            collectables[hash_code] = collectableDetected;
        }
        if (sos != null)
        {
            nextToSOS = true;
            sosSign = sos;
        }
        if (fire_ != null)
        {
            nextToFire = true;
            fire = fire_;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // description.text = "";
        // info.text = "";
        // action.text = "";

        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();
        SOS sos = collision.gameObject.GetComponent<SOS>();
        Fire fire_ = collision.gameObject.GetComponentInParent<Fire>();

        if (collectableDetected != null) 
        {
            int hash_code = collectableDetected.gameObject.GetHashCode();
            if (collectables.ContainsKey(hash_code))
            {
                collectables.Remove(hash_code);
            }
        }
        if (sos != null)
        {
            nextToSOS = false;
            sosSign = null;
        }
        if (fire_ != null)
        {
            nextToFire = false;
            fire = null;
        }
    }

}
