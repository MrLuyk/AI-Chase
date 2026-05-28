using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipRotation : MonoBehaviour
{
    [SerializeField] private float shrinkAmount = 0.5f;
    private float radius = 4.0f;
    public float distance = 5.0f;
    public LayerMask layerMask;

    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointAB;
    private float interpolateAmount;
    private HashSet<GameObject> enemiesInRange = new HashSet<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 origin = transform.position;

        HashSet<GameObject> currentEnemies = new HashSet<GameObject>();

        Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius);

        //Debug.Log("Hits found: " + hits.Length);

        if (gameObject.CompareTag("PatrolPoint"))
        {

            foreach (Collider2D hit in hits)
            {
                //Debug.Log(hit.name + " Tag: " + hit.tag);
                if (hit.CompareTag("Enemy"))
                {
                    //Debug.Log("Enemy is within " + gameObject.name);
                    currentEnemies.Add(hit.gameObject);

                    // Enemy just entered
                    if (!enemiesInRange.Contains(hit.gameObject))
                    {
                        Debug.Log("Enemy HAS Entered: " + gameObject.name);
                        onPipEnter();
                    }
                }
            }
        }

        enemiesInRange = currentEnemies;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    //void DrawCircle(Vector2 center, float radius, Color color)
    //{
    //    int segments = 16;
    //    for (int i = 0; i < segments; i++)
    //    {
    //        float angle1 = i * Mathf.PI * 2 / segments;
    //        float angle2 = (i + 1) * Mathf.PI * 2 / segments;
    //        Vector2 p1 = center + new Vector2(Mathf.Cos(angle1), Mathf.Sin(angle1)) * radius;
    //        Vector2 p2 = center + new Vector2(Mathf.Cos(angle2), Mathf.Sin(angle2)) * radius;
    //        Debug.DrawLine(p1, p2, color);
    //    }
    //}

    void onPipEnter()
    {
        // interpolation between pips
        interpolateAmount = (interpolateAmount + Time.deltaTime) % 1f;
        pointAB.position = Vector3.Lerp(pointA.position, pointB.position, interpolateAmount);
    }
}
