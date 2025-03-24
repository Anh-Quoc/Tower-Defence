using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private List<GameObject> DetectedEnemies;
    public GameObject CurrentTarget { get; private set; }

    void Start()
    {
        DetectedEnemies = new List<GameObject>();
        CurrentTarget = null;
    }

    private void UpdateTarget()
    {
        if (DetectedEnemies.Count > 0)
        {
            CurrentTarget = DetectedEnemies[0]; // Always target the first enemy in range
        }
        else
        {
            CurrentTarget = null; // No enemies in range
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            if (!DetectedEnemies.Contains(other.gameObject))
            {
                DetectedEnemies.Add(other.gameObject);
                UpdateTarget();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            if (DetectedEnemies.Contains(other.gameObject))
            {
                DetectedEnemies.Remove(other.gameObject);
                UpdateTarget(); // Automatically switch to the next enemy
            }
        }
    }
}
