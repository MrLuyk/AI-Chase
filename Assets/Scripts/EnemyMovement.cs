using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
        Vector2 origin = transform.position + transform.right * .75f;
        Vector2 direction = transform.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayDistance);

        Debug.DrawRay(origin, direction * rayDistance, Color.green);

        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name + " at point: " + hit.point);
            Debug.DrawRay(origin, direction * hit.distance, Color.red);
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }
}
