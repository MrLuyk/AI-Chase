using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEWEnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform player;
    public float rayDistance = 22f;
    public float rayDistanceShort = 3f;
    public float movementSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 origin = transform.position + transform.right * -.61f;// * -2.25f;
        Vector2 originLeft = transform.position + transform.up * -.61f;// * -2.25f;
        Vector2 originRight = transform.position + transform.up * .61f;// * -2.25f;
        Vector2 direction = -transform.right;
        Vector2 directionLeft = Quaternion.Euler(0, 0, 20) * direction;
        Vector2 directionRight = Quaternion.Euler(0, 0, -20) * direction;
        Vector2 leftSide = Quaternion.Euler(0, 0, 90) * direction;
        Vector2 rightSide = Quaternion.Euler(0, 0, -90) * direction;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, directionLeft, rayDistance);
        RaycastHit2D hitRight = Physics2D.Raycast(origin, directionRight, rayDistance);
        RaycastHit2D hitLeftSide = Physics2D.Raycast(originLeft, leftSide, rayDistanceShort);
        RaycastHit2D hitRightSide = Physics2D.Raycast(originRight, rightSide, rayDistanceShort);

        Debug.DrawRay(origin, direction * rayDistance, Color.green);
        Debug.DrawRay(origin, directionLeft * rayDistance, Color.green);
        Debug.DrawRay(origin, directionRight * rayDistance, Color.green);
        Debug.DrawRay(originLeft, leftSide * rayDistanceShort, Color.green);
        Debug.DrawRay(originRight, rightSide * rayDistanceShort, Color.green);


        if (hit.collider != null)// && !hit.collider.CompareTag("PatrolPoint")
        {
            //Debug.Log("Raycast hit: " + hit.collider.name + " at point: " + hit.point);
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            Debug.DrawRay(origin, directionLeft * hitLeft.distance, Color.red);
            Debug.DrawRay(origin, directionRight * hitRight.distance, Color.red);
            Debug.DrawRay(originLeft, leftSide * hitLeftSide.distance, Color.red);
            Debug.DrawRay(originRight, rightSide * hitRightSide.distance, Color.red);
        }

    }
}
