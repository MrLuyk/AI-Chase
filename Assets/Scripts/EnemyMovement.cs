using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Transform player;
    public float rayDistance = 22f;
    private string lastPoint = "Point1";
    public float movementSpeed = 5f;
    public float rotationSpeed = 90f;
    private Rigidbody2D rb;
    string[] points = { "Point1", "Point2", "Point3", "Point4", "Point5", "Point6"};

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 origin = transform.position + transform.up * -.51f;// * -2.25f;
        Vector2 direction = -transform.up;
        Vector2 directionLeft = Quaternion.Euler(0, 0, 25) * direction;
        Vector2 directionRight = Quaternion.Euler(0, 0, -25) * direction;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);
        RaycastHit2D hitLeft = Physics2D.Raycast(origin, directionLeft, rayDistance);
        RaycastHit2D hitRight = Physics2D.Raycast(origin, directionRight, rayDistance);

        Debug.DrawRay(origin, direction * rayDistance, Color.green);
        Debug.DrawRay(origin, directionLeft * rayDistance, Color.green);
        Debug.DrawRay(origin, directionRight * rayDistance, Color.green);

        if (hit.collider != null)
        {
            //Debug.Log("Raycast hit: " + hit.collider.name + " at point: " + hit.point);
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
            Debug.DrawRay(origin, directionLeft * hitLeft.distance, Color.red);
            Debug.DrawRay(origin, directionRight * hitRight.distance, Color.red);

            //if (hit.collider.gameObject.CompareTag("Point1") ||)

            foreach (string point in points)
            {
                if (hit.collider.gameObject.CompareTag("PatrolPoint"))
                {
                    lastPoint = hit.collider.gameObject.name;
                    print(lastPoint + " should be a patrol point");
                }
            }

            if (hit.collider.gameObject.CompareTag("PatrolPoint") && hit.distance >= 2f)
            {
                transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;
            }
            else if (hit.collider.gameObject.CompareTag("PatrolPoint") && hit.distance <= 2f)
            {
                transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;
            }

            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Vector2 dir = (player.position - transform.position).normalized;
                rb.velocity = dir * movementSpeed;
                rb.angularVelocity = 0f;
            }
            if (hitLeft.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("Player"))
            {
                RotateLeft();
            }
            if (hitRight.collider.gameObject.CompareTag("Player") && !hit.collider.gameObject.CompareTag("Player"))
            {
                RotateRight();
            }

            if (hitLeft.collider.gameObject.CompareTag("PatrolPoint") && hitLeft.distance >= 4f && !hit.collider.gameObject.CompareTag("Player"))
            {
                RotateLeft();
                //lastPoint = hit.collider.gameObject.name;
            }

            if (hitRight.collider.gameObject.CompareTag("PatrolPoint") && hitRight.distance >= 4f && !hit.collider.gameObject.CompareTag("Player"))
            {
                RotateRight();
                //lastPoint = hit.collider.gameObject.name;
            }

            if (hit.collider.gameObject.CompareTag("Wall") && hitLeft.collider.gameObject.CompareTag("Wall") && hitRight.collider.gameObject.CompareTag("Wall"))// && hit.distance <= 2f)
            {
                Debug.Log("The last point was: " + lastPoint);
                Vector2 direct = (GameObject.Find(points[LastPoint(lastPoint) - 1]).transform.position - transform.position).normalized;
                rb.velocity = direct * movementSpeed;
                if (lastPoint == points[points.Length - 1] && gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("Player"))
                {
                    lastPoint = points[0];
                }
                else if (lastPoint != points[points.Length - 1] && gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("Player"))
                {
                    lastPoint = points[LastPoint(lastPoint)];
                }
                print("NEW LASTPOINT: " + lastPoint);
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    void RotateLeft()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * 3);
        rb.angularVelocity = 0f;
    }

    void RotateRight()
    {
        transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime * 3);
        rb.angularVelocity = 0f;
    }

    int LastPoint(string pointy)
    {
        int value = 1;
        if (pointy != null || pointy != "Enemy")
        {
            string numberPart = pointy.Replace("Point", "");
            int.TryParse(numberPart, out value);
        }

        return value;
    }
}
