using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IApplyDamage
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _bloodParticle;
    
    private Player _target;

    public Player Target => _target;

    public event UnityAction<Enemy> Died;

    private void Die()
    {
        Died?.Invoke(this);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (this != null)
        {
            _bloodParticle.Play();
        }

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
