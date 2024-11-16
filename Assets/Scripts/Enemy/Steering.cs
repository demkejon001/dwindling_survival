using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Steering : MonoBehaviour
{
    [SerializeField] private int numDirections = 8;
    [SerializeField] private float avoidanceRadius = 4.0f;
    private Vector2[] directions;
    private float[] interestMap;
    private float[] dangerMap;
    EnemyChase enemy;

    void Start()
    {
        if (enemy == null)
        {
            enemy = GetComponentInParent<EnemyChase>();
        }

        InitializeDirections();
    }

    public Vector2 GetSteeringDirection(Vector2 interestDirection)
    {
        float lowestDanger = 1.0f;
        Vector2 pos = enemy.transform.position - new Vector3(0f, .5f, 0f);
        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D raycastHit = Physics2D.CircleCast(pos, .2f, directions[i], avoidanceRadius, 7);
            if (raycastHit.collider != null && !raycastHit.collider.isTrigger)
            {
                // // FOR DEBUGGING                
                // float x = raycastHit.centroid.x;
                // float y = raycastHit.centroid.y;
                // Vector2 topLeft = new Vector2(x - .5f, y + .5f);
                // Vector2 bottomRight = new Vector2(x + .5f, y - .5f);
                // Vector2 topRight = new Vector2(x + .5f, y + .5f);
                // Vector2 bottomLeft = new Vector2(x - .5f, y - .5f);
                // Debug.DrawLine(topLeft, bottomRight, Color.blue, 1.0f);
                // Debug.DrawLine(topRight, bottomLeft, Color.blue, 1.0f);
                // Debug.Log(raycastHit.collider.transform.position + " " + raycastHit.centroid);

                float danger = 1 - raycastHit.fraction;
                dangerMap[i] = danger;
                if (danger < lowestDanger)
                {
                    lowestDanger = danger;
                }
                // Debug.DrawLine(pos, pos + directions[i] * avoidanceRadius, Color.red);
            }
            else
            {
                dangerMap[i] = 0;
                lowestDanger = 0;
            }

            float interest = Vector2.Dot((interestDirection - pos).normalized, directions[i].normalized);
            interest = (interest + 1) / 2;
            // interest = Mathf.Clamp(interest, 0, 1.0f);
            interestMap[i] = interest;
        }

        Vector2 maxInterestDirection = Vector2.zero;
        float maxInterest = 0;
        for (int i = 0; i < directions.Length; i++)
        {
            float danger = dangerMap[i];
            if (danger > lowestDanger)
            {
                interestMap[i] = 0;
            }
            else
            {
                if (interestMap[i] > maxInterest)
                {
                    maxInterest = interestMap[i];
                    maxInterestDirection = directions[i];
                }
                // maxInterestDirection += directions[i] * interestMap[i];
            }
            // if (interestMap[i] > 0)
            // {
            //     Debug.DrawLine(pos, pos + directions[i]* avoidanceRadius, Color.green);
            // }
        }

        // Debug.DrawLine(pos, pos + maxInterestDirection * avoidanceRadius, Color.green);
        return maxInterestDirection.normalized;

    }


    void InitializeDirections()
    {
        directions = new Vector2[numDirections];
        interestMap = new float[numDirections];
        dangerMap = new float[numDirections];
        float twoPi = Mathf.PI * 2;
        float directionInterval = twoPi / numDirections;
        for (int i = 0; i < numDirections; i++)
        {
            float currentAngle = i * directionInterval;
            directions[i] = new Vector2(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle));

        }
    }
}
