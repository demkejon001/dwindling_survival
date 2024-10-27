using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public bool isInFireRadius = false;
    public float maxWarmth = 100.0f;
    public float warmth = 100.0f;
    public float warmthIncrementRate = 1.0f;
    public float warmthDecrementRate = 1.0f;
    public float health = 100.0f;
    public float maxHealth = 100.0f;
    public float hunger = 100.0f;
    public float maxHunger = 100.0f;
    public float hungerDecrementRate = .5f;
    public int[] inventory = new int[Enum.GetValues(typeof(ItemID)).Length];

    public StatusBar warmthBar;
    public StatusBar hungerBar;


    private void Start()
    {
        Array.Fill(inventory, 0);
    }

    public void AddObject(CollectableObject objectToAdd)
    {
        int index = (int)objectToAdd.id;
        inventory[index] += 1;
        GameManager.Instance.UpdateInventoryUI(objectToAdd.id);
    }

    public void RemoveObjectFromInventory(ItemID id)
    {
        int index = (int) id;
        if (inventory[index] > 0)
        {
            inventory[index] -= 1;
            GameManager.Instance.UpdateInventoryUI(id);
        }
    }

    public void DeathOfPlayer()
    {
        SceneManager.LoadScene(0);
    }

    public void TakeDamage(float damage)
    {
        health = Math.Max(0, health - damage);
    }

    public void Eat(float value)
    {
        hunger += Mathf.Min(value + hunger, maxHunger);
    }

    void Update()
    {
        if (isInFireRadius)
        {
            warmth = Mathf.Min(warmth + Time.deltaTime * warmthIncrementRate, maxWarmth);
        }
        else
        {
            warmth = Mathf.Max(warmth - Time.deltaTime * warmthDecrementRate, 0);
        }

        hunger -= hungerDecrementRate;

        if (warmthBar != null)
        {
            warmthBar.SetStatus(warmth / maxWarmth);
        }
        if (hungerBar != null)
        {
            hungerBar.SetStatus(hunger / maxHunger);
        }
        if (hunger <= 0 || warmth <= 0 || health <= 0)
        {
            DeathOfPlayer();
        }

    }
}
