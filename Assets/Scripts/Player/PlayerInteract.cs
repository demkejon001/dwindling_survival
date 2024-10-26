using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public TextMeshProUGUI description, info, action;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        description.text = "";
        info.text = "";
        action.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
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
            player.AddObject(collectableDetected);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        description.text = "";
        info.text = "";
        action.text = "";
    }
}
