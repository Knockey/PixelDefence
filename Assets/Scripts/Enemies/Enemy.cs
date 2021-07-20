using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(HitParticleEmitter))]
public class Enemy : MonoBehaviour, IDamageApplier
{
    [SerializeField] private int _health;
    
    private Player _target;
    private HitParticleEmitter _hitParticleEmitter;

    public Player Target => _target;

    public event UnityAction<Enemy> Died;

    private void Awake()
    {
        _hitParticleEmitter = GetComponent<HitParticleEmitter>();
    }

    private void Die()
    {
        Died?.Invoke(this);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;

        if (this != null)
        {
            _hitParticleEmitter.EmitParticle();
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
