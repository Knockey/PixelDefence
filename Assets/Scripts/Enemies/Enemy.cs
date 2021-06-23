using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IApplyDamage
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    
    private Player _target;

    public Player Target => _target;
    public int Reward => _reward;

    public event UnityAction<Enemy> Died;

    private void Die()
    {
        Died?.Invoke(this);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    public void Init(Player target)
    {
        _target = target;
    }
}
