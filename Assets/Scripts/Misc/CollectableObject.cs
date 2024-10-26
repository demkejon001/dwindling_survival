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
}
