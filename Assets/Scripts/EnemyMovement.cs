using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float rayDistance = 100f;
    private string lastWall;
    public float movementSpeed = 5f;
    public float rotationSpeed = 90f;

    void Update()
    {
        Vector2 origin = transform.position + transform.up * -2.25f;
        Vector2 direction = -transform.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);
        RaycastHit2D hit2 = Physics2D.Raycast(origin, direction, rayDistance);

        Debug.DrawRay(origin, direction * rayDistance, Color.green);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name + " at point: " + hit.point);
            Debug.DrawRay(origin, direction * hit.distance, Color.red);

            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                Debug.Log(hit.collider.name);
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;
            }

            if (hit.collider.gameObject.CompareTag("PatrolPoint"))
            {
                Debug.Log(hit.collider.name);
                transform.position += (Vector3)direction * movementSpeed * Time.deltaTime;
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
