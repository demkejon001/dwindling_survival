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

    void Start()
    {
        description.text = "";
        info.text = "";
        action.text = "";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && collectables.Count > 0)
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        InteractableObject objectDetected = collision.gameObject.GetComponent<InteractableObject>();
        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        description.text = "";
        info.text = "";
        action.text = "";

        CollectableObject collectableDetected = collision.gameObject.GetComponent<CollectableObject>();

        if (collectableDetected != null) 
        {
            int hash_code = collectableDetected.gameObject.GetHashCode();
            if (collectables.ContainsKey(hash_code))
            {
                collectables.Remove(hash_code);
            }
        }
    }

}
