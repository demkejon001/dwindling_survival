using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Timeline;

public class EnemyChase : MonoBehaviour
{
    public Transform playerTransform;
    public float speed = 10f;
    [SerializeField] public Rigidbody2D enemyRB;
    [SerializeField] private Collider2D enemyCollider;

    [SerializeField] public float MAX_SEE_AHEAD = 10;
    // private Vector2 attack_vector;

    [SerializeField] private float windUpDistance = 0.5f;
    [SerializeField] private float launchDistance = 3.0f;

    private Vector3 windUpPosition;
    private Vector3 launchPosition;

    [SerializeField] private float windUpDuration = 0.5f;
    [SerializeField] private float launchDuration = 0.2f;
    [SerializeField] private float attack_cooldown = 0.3f;

    [SerializeField] private float attackDamage = 10.0f;
    private bool canDamage = false;

    [SerializeField] float maxEvadeDistance = 30f;
    [SerializeField] float evadeDuration = 8f;

    private Steering steering;
    private Vector2 previousDirection = Vector2.zero;
    private Vector2 currentDirection = Vector2.zero;


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

    [SerializeField] private EnemyState enemyState;


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
        if (steering == null)
        {
            steering = GetComponentInParent<Steering>();
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
                // Vector2 direction = steering.GetSteeringDirection(playerTransform.position) * 4.0f;
                // // Debug.DrawLine(enemyRB.position, enemyRB.position + direction);
                // enemyRB.position = Vector2.MoveTowards(enemyRB.position, enemyRB.position + direction, Time.deltaTime * speed);
                goToLocation(playerTransform.position);
            }
        }

    }

    void goToLocation(Vector2 location)
    {
        Vector2 direction = steering.GetSteeringDirection(location) * 4.0f;
        currentDirection = Vector2.Lerp(currentDirection, direction, 1.8f * Time.deltaTime); 
        // Debug.DrawLine(enemyRB.position, enemyRB.position + direction, Color.blue);
        // Debug.DrawLine(enemyRB.position, enemyRB.position + currentDirection, Color.green);
        enemyRB.position = Vector2.MoveTowards(enemyRB.position, enemyRB.position + currentDirection, Time.deltaTime * speed);
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
            // // enemyRB.position = Vector2.MoveTowards(enemyRB.position, fleePosition, Time.deltaTime * speed);
            // Vector2 direction = enemyRB.position + steering.GetSteeringDirection(fleePosition) * 4.0f;
            // // Debug.DrawLine(enemyRB.position, direction);
            // enemyRB.position = Vector2.MoveTowards(enemyRB.position, direction, Time.deltaTime * speed);
            goToLocation(fleePosition);
            reachedDestination = Mathf.Approximately(0, (enemyRB.position - fleePosition).magnitude);
            distanceFromPlayer = (playerTransform.position - transform.position).magnitude;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        enemyState = EnemyState.None;
    }

}