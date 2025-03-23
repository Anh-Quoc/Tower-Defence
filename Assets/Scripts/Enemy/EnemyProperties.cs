using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyProperties : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 1f;
    [SerializeField] private float maxSpeed = 3f;
    [SerializeField] private float initialHP = 100f;
    [SerializeField] private float damage = 50f;
    [SerializeField] private int gold = 50;
    [SerializeField] UnityEvent onDeathEffect = null;

    private float speed;
    private float currentHP;
    private bool isDead;
    private int currentWaypointIndex = 0;

    public bool IsDead => isDead;
    public float CurrentHP => currentHP;
    public float Speed => speed;
    public float Damage => damage;

    public List<GameObject> Waypoints { get; set; }

    private EnemyMovement enemyManager;

    public void SetEnemyManager(EnemyMovement manager)
    {
        enemyManager = manager;
    }

    public GameObject PrefabReference { get; set; }

    private void Start()
    {
        speed = initialSpeed;
        currentHP = 0;
        isDead = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isDead) MoveTowardsWaypoint();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            GameManager.Instance.AddGold(gold);
            onDeathEffect?.Invoke();
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
        gameObject.SetActive(false);
        currentWaypointIndex = 0;
        if (Waypoints != null && Waypoints.Count > 0)
        {
            transform.position = Waypoints[0].transform.position;
        }
        enemyManager?.DeactivateEnemy(gameObject);
    }

    public void Respawn(float updatePercent)
    {
        isDead = false;
        currentHP = initialHP * (1 + updatePercent * 2);
        speed = initialSpeed * (1 + updatePercent * 2);
        if (speed > maxSpeed) speed = maxSpeed;
        gameObject.SetActive(true);
    }

    private void MoveTowardsWaypoint()
    {
        if (Waypoints == null || Waypoints.Count == 0 || currentWaypointIndex >= Waypoints.Count) return;

        Transform targetWaypoint = Waypoints[currentWaypointIndex].transform;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        // Rotate the enemy to face the target waypoint
        Vector3 direction = targetWaypoint.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= Waypoints.Count)
            {
                Die();
                GameManager.Instance?.TakeDamage(damage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.CompareTag("Bullet"))
        // {
        //     TakeDamage(collision.gameObject.GetComponent<BulletController>().Damage);
        // }
    }
}
