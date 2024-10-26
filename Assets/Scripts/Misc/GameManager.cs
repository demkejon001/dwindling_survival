using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private TextMeshProUGUI woodValue;
    [SerializeField]
    private TextMeshProUGUI berryValue;
    [SerializeField]
    private TextMeshProUGUI rockValue;
    [SerializeField]
    private TextMeshProUGUI waterValue;
    [SerializeField]
    private Player player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void UpdateInventoryUI(ItemID id)
    {
        int inventoryIndex = (int) id;
        switch (id)
        {
            case ItemID.Berry:
                berryValue.text = player.inventory[inventoryIndex].ToString(); 
                break;
            case ItemID.Wood:
                woodValue.text = player.inventory[inventoryIndex].ToString();
                break;
            case ItemID.Water:
                waterValue.text = player.inventory[inventoryIndex].ToString();
                break;
            case ItemID.Rock:
                rockValue.text = player.inventory[inventoryIndex].ToString();
                break;
        }
    }
    // public void UpdateInventoryUI(int inventoryIndex)
    // {
    //     switch (inventoryIndex)
    //     {
    //         case 0:
    //             berryValue.text = player.inventory[inventoryIndex].ToString(); 
    //             break;
    //         case 1:
    //             woodValue.text = player.inventory[inventoryIndex].ToString();
    //             break;
    //         case 2:
    //             waterValue.text = player.inventory[inventoryIndex].ToString();
    //             break;
    //         case 3:
    //             rockValue.text = player.inventory[inventoryIndex].ToString();
    //             break;
    //     }
    // }


    
}
