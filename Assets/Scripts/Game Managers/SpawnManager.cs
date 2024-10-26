using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemiesToSpawn;
    [SerializeField] private GameObject[] pickupsToSpawn;
    public Transform enemySpawnTransform;
    private float recoveryRate = 5;
    private int indexOfEnemySpawn;
    private int indexOfPickupSpawn;

    // Start is called before the first frame update
    void Start()
    {
        indexOfEnemySpawn = Random.Range(0, enemiesToSpawn.Length);
        indexOfPickupSpawn = Random.Range(0, pickupsToSpawn.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if(recoveryRate > 0)
        {
            recoveryRate -= Time.deltaTime;
            return;
        }
        Instantiate(enemiesToSpawn[indexOfEnemySpawn], enemySpawnTransform.position, Quaternion.identity);
        Instantiate(pickupsToSpawn[indexOfPickupSpawn]);
        recoveryRate = 1.5f;
        ResetIndexValue();
    }

    public void ResetIndexValue()
    {
        indexOfEnemySpawn = Random.Range(0, enemiesToSpawn.Length);
        indexOfPickupSpawn = Random.Range(0, pickupsToSpawn.Length);
    }
}
