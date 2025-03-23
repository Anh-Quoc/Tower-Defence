using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int poolingSizeEachEnemy = 50;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private List<WaveProperties> waveProperties;
    [SerializeField] private List<GameObject> waypoints;
    private Dictionary<GameObject, Queue<GameObject>> enemyPool = new Dictionary<GameObject, Queue<GameObject>>();
    private List<GameObject> activeObjects = new List<GameObject>();
    private WaveProperties currentWave;
    private float spawnWaveDelay;
    private Coroutine spawnRoutine;

    private void Start()
    {
        InitializeObjectPool();
    }

    private void Update()
    {
        if(!GameManager.Instance.IsGameOver)
            HandleWaveSpawning();
    }

    private void InitializeObjectPool()
    {
        foreach (var prefab in enemyPrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();

            for (int i = 0; i < poolingSizeEachEnemy; i++)
            {
                GameObject enemy = Instantiate(prefab, waypoints[0].transform.position, Quaternion.identity);
                enemy.GetComponent<EnemyProperties>().Waypoints = waypoints;
                pool.Enqueue(enemy);
            }

            enemyPool[prefab] = pool;
        }
    }

    private void HandleWaveSpawning()
    {
        if (currentWave == null)
        {
            GameManager.Instance.NextWave();
            waveProperties.Sort((a, b) => a.waveIndex.CompareTo(b.waveIndex));

            currentWave = waveProperties[waveProperties.Count - 1];
            for (int i = 0; i < waveProperties.Count; i++)
            {
                if (waveProperties[i].waveIndex > GameManager.Instance.GetCurrentWave())
                {
                    currentWave = waveProperties[i - 1];
                    break;
                }
            }

            spawnWaveDelay = currentWave.waveDelay;
            if (spawnRoutine != null) StopCoroutine(spawnRoutine);
            spawnRoutine = StartCoroutine(SpawnEnemies());
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
            enemyProps.Respawn((float)GameManager.Instance.GetCurrentWave()/10);
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