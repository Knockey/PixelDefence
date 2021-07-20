using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new List<Transform>();
    [SerializeField] private List<Wave> _waves = new List<Wave>();
    [SerializeField] private Player _target;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private int _enemiesInWaveCount;
    private float _timeAfterLastSpawn;
    private int _spawnedEnemiesCount;
    private int _diedEnemiesCount = 0;

    public event UnityAction<int, int> EnemyDied;
    public event UnityAction WaveStarted;
    public event UnityAction WaveFinished;
    public event UnityAction GameFinished;

    private void Start()
    {
        _currentWave = SetWave(_currentWaveIndex);
        EnemyDied?.Invoke(_diedEnemiesCount, _enemiesInWaveCount);
    }

    private void Update()
    {
        if (_currentWave == null)
        {
            return;
        }

        _timeAfterLastSpawn += Time.deltaTime;
        TrySpawnEnemy();

        if (_spawnedEnemiesCount >= _enemiesInWaveCount)
        {
            _currentWave = null;
        }
    }

    private void TrySpawnEnemy()
    {
        if (_timeAfterLastSpawn >= _currentWave.SpawnDelay)
        {
            InstantiateEnemy();

            _spawnedEnemiesCount++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void InstantiateEnemy()
    {
        int nextEnemyIndex = Random.Range(0, _currentWave.Enemies.Count);
        int spawnPointIndex = Random.Range(0, _spawnPoints.Count);

        Enemy enemy = Instantiate(_currentWave.Enemies[nextEnemyIndex], _spawnPoints[spawnPointIndex].position, Quaternion.identity, _spawnPoints[spawnPointIndex]);
        enemy.Init(_target);
        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;

        _diedEnemiesCount++;
        EnemyDied?.Invoke(_diedEnemiesCount, _enemiesInWaveCount);

        if (_diedEnemiesCount >= _enemiesInWaveCount)
        {
            CheckGameOrWaveFinished();
        }
    }

    private void CheckGameOrWaveFinished()
    {
        if (_waves.Count - 1 > _currentWaveIndex)
        {
            WaveFinished?.Invoke();
        }
        else
        {
            GameFinished?.Invoke();
        }
    }

    private Wave SetWave(int index)
    {
        _enemiesInWaveCount = _waves[index].EnemiesCount;
        return _waves[index];
    }

    public void NextWave()
    {
        _currentWave = SetWave(++_currentWaveIndex);
        _spawnedEnemiesCount = 0;
        _diedEnemiesCount = 0;

        WaveStarted?.Invoke();
        EnemyDied?.Invoke(_diedEnemiesCount, _enemiesInWaveCount);
    }
}
