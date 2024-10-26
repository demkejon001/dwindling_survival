using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyChase : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 3f;
    [SerializeField] 
    private Rigidbody2D enemyRB;

    [SerializeField]
    public float MAX_SEE_AHEAD = 10;
    // private Vector2 attack_vector;

    [SerializeField]
    private float windUpDistance = 0.5f;
    [SerializeField]
    private float launchDistance = 1.0f;

    private Vector3 windUpPosition;
    private Vector3 launchPosition;

    private float windUpDuration = 0.5f;
    private float launchDuration = 0.5f;
    private float attack_cooldown = 0.1f;

    enum EnemyState
    {
        Attack,
        Stalk,
        Evade,
        Pursue,
        None,
    }

    private EnemyState enemyState;


    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyState = EnemyState.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.None)
        {
            float player_distance = (playerTransform.position - transform.position).magnitude;

            if (player_distance < launchDistance * .7)
            {
                enemyState = EnemyState.Attack;
                StartCoroutine(Attack());
            }
            else
            {
                enemyRB.position = Vector2.MoveTowards(transform.position, playerTransform.position, Time.deltaTime * speed);
            }
        }

    }


    IEnumerator Attack()
    {

        Vector2 attack_vector = (playerTransform.position - transform.position).normalized;
        Vector2 startPosition = enemyRB.position;
        windUpPosition = enemyRB.position - (attack_vector * windUpDistance);
        launchPosition = enemyRB.position + (attack_vector * launchDistance);

        float elapsedTime = 0.0f;
        // Debug.Log("WindUp" + transform.position + " " + windUpPosition);
        // Debug.DrawLine(transform.position, windUpPosition, Color.red, 1.0f);
        while (elapsedTime < windUpDuration)
        {
            enemyRB.position = Vector2.Lerp(startPosition, windUpPosition, elapsedTime / windUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // Debug.Log("Attac" + transform.position + " " + launchPosition);
        // Debug.DrawLine(transform.position, launchPosition, Color.red, 1.0f);
        elapsedTime = 0.0f;
        while (elapsedTime < launchDuration)
        {
            enemyRB.position = Vector2.Lerp(windUpPosition, launchPosition, elapsedTime / launchDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // Debug.Log("Cooldown");
        elapsedTime = 0.0f;
        while (elapsedTime < attack_cooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        enemyState = EnemyState.None;
        
    }

}
