using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyChase : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 10f;
    [SerializeField] 
    private Rigidbody2D enemyRB;
    [SerializeField]
    private Collider2D enemyCollider;

    [SerializeField]
    public float MAX_SEE_AHEAD = 10;
    // private Vector2 attack_vector;

    [SerializeField]
    private float windUpDistance = 0.5f;
    [SerializeField]
    private float launchDistance = 3.0f;

    private Vector3 windUpPosition;
    private Vector3 launchPosition;

    [SerializeField]
    private float windUpDuration = 0.5f;
    [SerializeField]
    private float launchDuration = 0.2f;
    [SerializeField]
    private float attack_cooldown = 0.3f;

    [SerializeField]
    private float attackDamage = 10.0f;
    private bool canDamage = false;

    [SerializeField]
    float maxEvadeDistance = 30f;
    [SerializeField]
    float evadeDuration = 8f;


    // [SerializeField]
    // public float rotationRate = 5f;  // time it takes to make a full revolution around character
    // [SerializeField]
    // public float minRadius = 3f;
    // [SerializeField]
    // public float maxRadius = 6f;
    // [SerializeField]
    // public float radiusChangeSpeed = 0.01f;
    // private float currentRadius = 4f;
    // private float currentXRadius = 4f;
    // private float currentYRadius = 4f;
    // private float xRadius = 4f;
    // private float yRadius = 4f;
    // private float angle;

    enum EnemyState
    {
        Attack,
        Stalk,
        Evade,
        Pursue,
        Zigzag,
        None,
    }

    [SerializeField]
    private EnemyState enemyState;


    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyState.None;
        if (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (enemyRB == null)
        {
            enemyRB = GetComponent<Rigidbody2D>();
        }
        if (enemyCollider == null)
        {
            enemyCollider = GetComponent<Collider2D>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (canDamage)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.TakeDamage(attackDamage);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.None)
        {
            // float player_distance = (playerTransform.position - enemyRB.position).magnitude;
            float player_distance = (playerTransform.position - transform.position).magnitude;

            if (player_distance < launchDistance * .7)
            {
                // StartCoroutine(Stalk());
                enemyState = EnemyState.Attack;
                StartCoroutine(Attack());
            }
            else
            {
                enemyRB.position = Vector2.MoveTowards(enemyRB.position, playerTransform.position, Time.deltaTime * speed);
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
        // Debug.Log("WindUp" + enemyRB.position + " " + windUpPosition);
        // Debug.DrawLine(enemyRB.position, windUpPosition, Color.red, 1.0f);
        while (elapsedTime < windUpDuration)
        {
            enemyRB.position = Vector2.Lerp(startPosition, windUpPosition, elapsedTime / windUpDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        // Debug.Log("Attac" + enemyRB.position + " " + launchPosition);
        // Debug.DrawLine(enemyRB.position, launchPosition, Color.red, 1.0f);
        elapsedTime = 0.0f;
        canDamage = true;
        while (elapsedTime < launchDuration)
        {
            enemyRB.position = Vector2.Lerp(windUpPosition, launchPosition, elapsedTime / launchDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }
        canDamage = false;

        // Debug.Log("Cooldown");
        elapsedTime = 0.0f;
        while (elapsedTime < attack_cooldown)
        {
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        enemyState = EnemyState.Evade;
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        Vector2 fleeDirection = -(playerTransform.position - transform.position).normalized;
        float distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
        Vector2 fleePosition = enemyRB.position + fleeDirection * maxEvadeDistance;
        float elapsedTime = 0.0f;
        bool reachedDestination = false;
        while (elapsedTime < evadeDuration && distanceFromPlayer < maxEvadeDistance && !reachedDestination)
        {
            enemyRB.position = Vector2.MoveTowards(enemyRB.position, fleePosition, Time.deltaTime * speed);
            reachedDestination = Mathf.Approximately(0, (enemyRB.position - fleePosition).magnitude);
            distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        enemyState = EnemyState.None;
    }

    // private bool AreFloatsClose(float a, float b, float tolerance = .0001f)
    // {
    //     return Mathf.Abs(a - b) < tolerance;
    // }

    // IEnumerator Stalk()
    // {

    //     while (true)
    //     {
    //         angle = angle + (6.284f * Time.deltaTime / rotationRate);
    //         if (angle > 6.284f)
    //         {
    //             angle = 0f;
    //         }

    //         // if (Mathf.Approximately(currentXRadius, xRadius))
    //         if (AreFloatsClose(currentXRadius, xRadius, radiusChangeSpeed))
    //         {
    //             xRadius = UnityEngine.Random.Range(minRadius, maxRadius);
    //         }
    //         else
    //         {
    //             currentXRadius += Mathf.Sign(xRadius - currentXRadius) * radiusChangeSpeed;
    //         }
    //         if (AreFloatsClose(currentYRadius, yRadius, radiusChangeSpeed))
    //         {
    //             yRadius = UnityEngine.Random.Range(minRadius, maxRadius);
    //         }
    //         else
    //         {
    //             currentYRadius += Mathf.Sign(yRadius - currentYRadius) * radiusChangeSpeed;
    //         }

    //         Vector3 offset = new Vector3(Mathf.Cos(angle) * currentXRadius, Mathf.Sin(angle) * currentYRadius, 0);

    //         // Update the position of the rotating object
    //         Debug.DrawLine(enemyRB.position, playerTransform.position + offset, Color.red, .1f);
    //         Debug.DrawLine(playerTransform.position, playerTransform.position + offset, Color.green, .1f);
    //         // enemyRB.position = Vector2.MoveTowards(enemyRB.position, playerenemyRB.position + offset, Time.deltaTime * speed);
    //         enemyRB.position = Vector2.MoveTowards(enemyRB.position, playerTransform.position + offset, Time.deltaTime * speed);
    //         // enemyRB.position += (playerTransform.position + offset - enemyRB.position).normalized * Time.deltaTime * speed;
    //         yield return null;
    //     }
        
    // }


}
