using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    private GameObject bulletInst;


    private Vector2 direction;
    private Vector2 worldPosition;
    private float angle;

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            bulletInst = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        }
    }

    private void HandleGunRotation()
    {
        angle = MathF.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)transform.position).normalized;
    }
}
