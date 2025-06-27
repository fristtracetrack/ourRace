using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentwaypointIndex = 0;

    [SerializeField] private float speed = 3f;

    private void Update()
    {
        if (Vector2.Distance(waypoints[currentwaypointIndex].transform.position, transform.position) < 0.1f)
        {
            currentwaypointIndex++;
            if (currentwaypointIndex >= waypoints.Length)
            {
                currentwaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentwaypointIndex].transform.position, Time.deltaTime * speed);
    }
}
