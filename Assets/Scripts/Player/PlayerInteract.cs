using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public TextMeshProUGUI description, info, action;
    public Player player;
    public Dictionary<int, CollectableObject> collectables = new Dictionary<int, CollectableObject>();
    public bool nextToSOS;
    private SOS sosSign;

    void Start()
    {
        description.text = "";
        info.text = "";
        action.text = "";
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject objectDetected = collision.gameObject.GetComponent<InteractableObject>();
        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();
        SOS sos = collision.gameObject.GetComponent<SOS>();

        if (objectDetected != null)
        {
            description.text = objectDetected.typeOfInteractable;
            info.text = objectDetected.interactableInfo;
            action.text = objectDetected.actionToDo;
        }
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        description.text = "";
        info.text = "";
        action.text = "";

        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();
        SOS sos = collision.gameObject.GetComponent<SOS>();

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
    }

}
