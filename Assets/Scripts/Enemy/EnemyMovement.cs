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

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(spawnWaveDelay);

        foreach (var enemyData in currentWave.enemies)
        {

            yield return new WaitForSeconds(enemyData.spawnDelay);

            for (int i = 0; i < enemyData.amount; i++)
            {
                ActivateEnemy(enemyData.enemyType);
                yield return new WaitForSeconds(enemyData.spawnRate);
            }
        }
    }

    private void ActivateEnemy(GameObject enemyType)
    {
        if (enemyPool.ContainsKey(enemyType) && enemyPool[enemyType].Count > 0)
        {
            GameObject enemy = enemyPool[enemyType].Dequeue();
            EnemyProperties enemyProps = enemy.GetComponent<EnemyProperties>();
            enemyProps.SetEnemyManager(this);
            enemyProps.PrefabReference = enemyType;
            enemyProps.Respawn((float)GameManager.Instance.GetCurrentWave() / 10);
            activeObjects.Add(enemy);
        }
    }

    public void DeactivateEnemy(GameObject enemy)
    {
        activeObjects.Remove(enemy);

        EnemyProperties enemyProps = enemy.GetComponent<EnemyProperties>();
        if (enemyProps != null && enemyProps.PrefabReference != null && enemyPool.ContainsKey(enemyProps.PrefabReference))
        {
            enemyPool[enemyProps.PrefabReference].Enqueue(enemy);
        }
        else
        {
            Debug.LogError("Prefab reference not found in enemyPool for: " + enemy.name);
        }
        if (activeObjects.Count == 0) currentWave = null;
    }

    public void StopMovementForSeconds(float seconds)
    {
        StartCoroutine(StopMovementCoroutine(seconds));
    }

    private IEnumerator StopMovementCoroutine(float seconds)
    {
        foreach (GameObject enemy in activeObjects)
        {
            EnemyProperties enemyProps = enemy.GetComponent<EnemyProperties>();
            if (enemyProps != null)
            {
                enemyProps.isStopped = true;
            }
        }
        yield return new WaitForSeconds(seconds);
        foreach (GameObject enemy in activeObjects)
        {
            EnemyProperties enemyProps = enemy.GetComponent<EnemyProperties>();
            if (enemyProps != null)
            {
                enemyProps.isStopped = false;
            }
        }
    }

    // public void SlowDownForSeconds(float slowSpeed, float duration)
    // {
    //     StartCoroutine(SlowDownCoroutine(slowSpeed, duration));
    // }

    // private IEnumerator SlowDownCoroutine(float slowSpeed, float duration)
    // {
    //     float originalSpeed = speed; 
    //     speed = slowSpeed; 
    //     yield return new WaitForSeconds(duration); 
    //     speed = originalSpeed; 
    // }
}
