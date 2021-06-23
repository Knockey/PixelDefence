using UnityEngine;

public class HealthBar : Bar
{
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.HealthChanged += ValueChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= ValueChanged;        
    }
}
