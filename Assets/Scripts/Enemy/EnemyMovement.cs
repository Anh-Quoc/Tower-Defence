using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] List<GameObject> waypoints;
    int currentWaypointIndex = 0;
    float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        speed = enemy.GetComponent<EnemyProperties>().getSpeed();
        enemy.transform.position = waypoints[currentWaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Count == 0 || currentWaypointIndex == waypoints.Count) return;
        MoveTowardsWaypoint();
    }

    private void MoveTowardsWaypoint()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex].transform;
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, targetWaypoint.position, speed * Time.deltaTime);
        
        // Rotate the enemy to face the target waypoint
        Vector3 direction = targetWaypoint.position - enemy.transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Calculate angle in degrees
        enemy.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Vector3.Distance(enemy.transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}
