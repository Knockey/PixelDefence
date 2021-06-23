using UnityEngine;

public class ProgressBar : Bar
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.EnemyDied += ValueChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemyDied -= ValueChanged;        
    }
}
