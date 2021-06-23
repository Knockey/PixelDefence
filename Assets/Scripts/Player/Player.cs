using System;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IApplyDamage
{
    [SerializeField] private int _maxHealth;

    private int _health;
    private int _money;
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;
    public int Money => _money;

    public event UnityAction Died;
    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0)
        {
            _isAlive = false;
            Died?.Invoke();
        }
    }

    public void AddMoney(int reward)
    {
        _money += reward;
    }
}
