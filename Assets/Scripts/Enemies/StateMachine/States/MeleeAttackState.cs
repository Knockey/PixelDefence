using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MeleeAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;

    private float _timePassedAfterAttack;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_timePassedAfterAttack <= 0)
        {
            Attack();
            _timePassedAfterAttack = _attackDelay;
        }

        _timePassedAfterAttack -= Time.deltaTime;
    }

    private void Attack()
    {
        _animator.Play("Attack");

        if (Target.IsVulnerable)
        {
            Target.ApplyDamage(_damage);
        }
    }
}
