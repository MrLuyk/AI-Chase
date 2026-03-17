using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public Transform player;
    public float rayDistance = 22f;
    private string lastPoint = "Point1";
    private string oldLastPoint = "Point6";
    public float movementSpeed = 5f;
    public float rotationSpeed = 90f;
    private Rigidbody2D rb;
    string[] points = { "Point1", "Point2", "Point3", "Point4", "Point5", "Point6"};
    private float delayTime = 2.0f;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 origin = transform.position + transform.right * -.61f;// * -2.25f;
        Vector2 direction = -transform.right;
        Vector2 directionLeft = Quaternion.Euler(0, 0, 20) * direction;
        Vector2 directionRight = Quaternion.Euler(0, 0, -20) * direction;

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

            //foreach (string point in points)
            //{
            //    if (hit.collider.gameObject.CompareTag("PatrolPoint"))
            //    {
            //        lastPoint = hit.collider.gameObject.name;
            //        print(lastPoint + " should be a patrol point");
            //    }
            //    if (hitLeft.collider.gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("PatrolPoint"))
            //    {
            //        lastPoint = hitLeft.collider.gameObject.name;
            //        print(lastPoint + " should be a patrol pointleft");

            //    }
            //    if (hitRight.collider.gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("PatrolPoint"))
            //    {
            //        lastPoint = hitRight.collider.gameObject.name;
            //        print(lastPoint + " should be a patrol pointright");

            //    }
            //}

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

            if (hitLeft.collider.gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("Player"))// && hitLeft.distance >= 4f)
            {
                RotateLeft();
                //lastPoint = hit.collider.gameObject.name;
            }

            if (hitRight.collider.gameObject.CompareTag("PatrolPoint") && !hit.collider.gameObject.CompareTag("Player"))// && hitRight.distance >= 4f)
            {
                RotateRight();
                //lastPoint = hit.collider.gameObject.name;
            }

            if (hit.collider.gameObject.CompareTag("Wall") && hitLeft.collider.gameObject.CompareTag("Wall") && hitRight.collider.gameObject.CompareTag("Wall"))// && hit.distance <= 2f)
            {
                Debug.Log("The last point was: " + lastPoint);
                Vector2 direct = (GameObject.Find(points[LastPoint(lastPoint) - 1]).transform.position - transform.position).normalized;
                rb.velocity = direct * movementSpeed;
                timer += Time.deltaTime;
                if (hit.distance <= 2.0f && hit.collider.gameObject.name != oldLastPoint && timer >= delayTime)
                {
                    NextPoint();
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
        if (pointy != null)
        {
            string numberPart = pointy.Replace("Point", "");
            int.TryParse(numberPart, out value);
        }

        return value;
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        print("ON TRIGGER ENTER");
        if (collision.gameObject.CompareTag("PatrolPoint"))
        {
            NextPoint();
            print("newnextpoint" + lastPoint);
        }
    }

    void NextPoint()
    {
        oldLastPoint = lastPoint;
        if (lastPoint == points[0])
        {
            print("1: " + lastPoint);
            lastPoint = points[1];
        }
        else if (lastPoint == points[1])
        {
            print("2: " + lastPoint);
            lastPoint = points[2];
        }
        else if (lastPoint == points[2])
        {
            print("3: " + lastPoint);
            lastPoint = points[3];
        }
        else if (lastPoint == points[3])
        {
            print("4: " + lastPoint);
            lastPoint = points[4];
        }
        else if (lastPoint == points[4])
        {
            print("5: " + lastPoint);
            lastPoint = points[5];
        }
        else if (lastPoint == points[5])
        {
            print("6: " + lastPoint);
            lastPoint = points[0];
        }
    }
}
