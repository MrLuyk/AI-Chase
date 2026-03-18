using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleMovement : MonoBehaviour
{
    public float radius = 1.0f;
    public float distance = 5.0f;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = transform.right;
        Vector2 origin = transform.position;

        RaycastHit2D hit = Physics2D.CircleCast(origin, radius, direction, distance, layerMask);

        float hitDistance = (hit.collider != null) ? hit.distance : distance;

        Color color = (hit.collider != null) ? Color.red : Color.green;

        Debug.DrawRay(origin, direction * hitDistance, color);

        // Draw circles at start and end to visualize radius
        DrawCircle(origin, radius, color);
        DrawCircle(origin + (direction * hitDistance), radius, color);

        if (hit.collider != null)
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }

    void DrawCircle(Vector2 center, float radius, Color color)
    {
        int segments = 16;
        for (int i = 0; i < segments; i++)
        {
            float angle1 = i * Mathf.PI * 2 / segments;
            float angle2 = (i + 1) * Mathf.PI * 2 / segments;
            Vector2 p1 = center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
            Vector2 p2 = center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;
            Debug.DrawLine(p1, p2, color);
        }
    }
}
