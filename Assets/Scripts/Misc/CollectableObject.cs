using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class CollectableObject : MonoBehaviour
{
    public int hasAmount;

    private void Start()
    {
        hasAmount = 0;
        objectCountNum.text = hasAmount.ToString();
    }

    public enum ItemID
    {
        Berry,
        Wood,
        Water,
        Rock
    }

    public ItemID id = ItemID.Berry;

    public TextMeshProUGUI objectCountNum;

    public void Collect()
    {
        hasAmount++;
        objectCountNum.text = hasAmount.ToString();
        Destroy(gameObject);
    }
}
