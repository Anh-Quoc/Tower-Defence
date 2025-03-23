using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WaveEnemyProperties
{
    public GameObject enemyType;
    [Range(0, 100)]
    public int amount;
    public float spawnRate;
    public float spawnDelay;
}
