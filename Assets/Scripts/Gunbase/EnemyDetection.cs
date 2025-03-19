using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private List<GameObject> detectedEnemies = new List<GameObject>();
    public GameObject CurrentTarget { get; private set; }

    private void UpdateTarget()
    {
        if (detectedEnemies.Count > 0)
        {
            CurrentTarget = detectedEnemies[0]; // Always target the first enemy in range
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
            if (!detectedEnemies.Contains(other.gameObject))
            {
                detectedEnemies.Add(other.gameObject);
                Debug.Log($"Enemy {detectedEnemies.Count} Entered: {other.name}");
                UpdateTarget();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Enemy"))
        {
            if (detectedEnemies.Contains(other.gameObject))
            {
                detectedEnemies.Remove(other.gameObject);
                Debug.Log($"Enemy Exited: {other.name}");
                UpdateTarget(); // Automatically switch to the next enemy
            }
        }
    }
}
