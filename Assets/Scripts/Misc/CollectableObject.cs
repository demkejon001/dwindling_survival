using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ItemID
{
    Berry,
    Wood,
    Water,
    Rock
}
public class CollectableObject : MonoBehaviour
{
    public int hasAmount;
    public ItemID id;

    public TextMeshProUGUI objectCountNum;

    private void Start()
    {
        hasAmount = 0;
        objectCountNum.text = hasAmount.ToString();
    }

    

    

    public void Collect()
    {
        hasAmount++;
        objectCountNum.text = hasAmount.ToString();
        Destroy(gameObject);
    }
}
