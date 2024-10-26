using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public PlayerMovement playerMovement;
    [SerializeField] private Transform spawnPoint1, spawnPoint2;

    // Update is called once per frame
    void Update()
    {
        if (InputManager.instance.controls.Shooting.Shoot.WasPressedThisFrame())
        {
            if(playerMovement.moveInput.x <= -1)
            {
                GameObject spawnedProjectile = Instantiate(projectilePrefab, spawnPoint1.position, Quaternion.identity);
                spawnedProjectile.GetComponent<Projectile>().directionalMultiplier = -1;
            }
            else if(playerMovement.moveInput.x >= 1)
            {
                GameObject spawnedProjectile = Instantiate(projectilePrefab, spawnPoint2.position, Quaternion.identity);
                spawnedProjectile.GetComponent<Projectile>().directionalMultiplier = 1;
            }
        }
    }
}
