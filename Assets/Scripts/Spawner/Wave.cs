using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _enemiesCount;

    public List<Enemy> Enemies => _enemies;
    public float SpawnDelay => _spawnDelay;
    public int EnemiesCount => _enemiesCount;
}
