using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 3f;
    [SerializeField] 
    private Rigidbody2D enemyRB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
    }
}
