using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    protected static float gameTimer = (6f*60f);
    public static float gameCycle;

    [Header("Time")]
    public string amPm;
    public string hourDisplay;
    public string minuteDisplay;
    [SerializeField] private float timeSpeed = 1f;
    private int hourTimer;
    private int minuteTimer;

    [Header("Temperature")]
    public float minTemp = 5f;
    public float maxTemp = 30f;
    public float currentTemp;
    [SerializeField] private float tempRange;
    [SerializeField] private float warmthLevel;


    [Header("Other Items")]
    [SerializeField]
    private TextMeshProUGUI woodValue;
    [SerializeField]
    private TextMeshProUGUI berryValue;
    [SerializeField]
    private TextMeshProUGUI rockValue;
    // [SerializeField]
    // private TextMeshProUGUI waterValue;
    [SerializeField]
    private Player player;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
        }
        tempRange = maxTemp - minTemp;
    }
    private void Update()
    {
        ClockCounter();
        TemperatureRegulation();
        
    }

    public void UpdateInventoryUI(ItemID id)
    {
        int inventoryIndex = (int) id;
        switch (id)
        {
            case ItemID.Berry:
                berryValue.text = player.inventory[inventoryIndex].ToString() + " Food"; 
                break;
            case ItemID.Wood:
                woodValue.text = player.inventory[inventoryIndex].ToString() + " Wood";
                break;
            // case ItemID.Water:
            //     waterValue.text = player.inventory[inventoryIndex].ToString();
            //     break;
            case ItemID.Rock:
                rockValue.text = player.inventory[inventoryIndex].ToString() + " Rock";
                break;
        }
    }

    private void TemperatureRegulation()
    {
        if (amPm == "AM")
        {
            if(hourTimer == 0)
            {
                currentTemp = minTemp;

            }
            else
            {
                currentTemp = minTemp + (((hourTimer) / 12f) * tempRange);
            }
            
        } 
        else 
        {
            if(hourTimer == 12)
            {
                currentTemp = maxTemp;
            } else
            {
                currentTemp = maxTemp - (((hourTimer-12) / 12f) * tempRange);
            }

            
        }

        



    }
    public float GameCycle()
    {
        //I need this to reflect the correct AM/PM setting
        Debug.Log( 1 - (float)(Mathf.Cos((Mathf.PI /(60*12) * (gameTimer+12))) / 2 + .5));
        return gameCycle = 1f - (float)(Mathf.Cos((Mathf.PI /(60*12) * (gameTimer+12))) / 2 + .5);

    }

    private void ClockCounter()
    {
        gameTimer += Time.deltaTime *timeSpeed;
        hourTimer = ((int)(gameTimer / 60));
        minuteTimer = (int)(gameTimer % 60);
        if (hourTimer == 0)
        {
            hourDisplay = "12";
        }
        else
        {
            hourDisplay = hourTimer > 12 ? (hourTimer - 12).ToString() : hourTimer.ToString();
        }

        minuteDisplay = minuteTimer.ToString();

        if (gameTimer > (60 * 24))
        {
            gameTimer = 0;
        }
        amPm = hourTimer > 11 ? "PM" : "AM";
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
