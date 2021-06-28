using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RangeAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDelay;
    [SerializeField] private Fireball _fireball;
    [SerializeField] private Transform _shootPoint;

    private float _timePassedAfterAttack;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _timePassedAfterAttack += Time.deltaTime;

        if (_timePassedAfterAttack >= _attackDelay)
        {
            Attack();
            _timePassedAfterAttack = 0;
        }
    }

    private void Attack()
    {
        _animator.Play("Attack");

        var projectile = Instantiate(_fireball, _shootPoint.position, Quaternion.identity);
        projectile.Init(_shootPoint, _damage, Target);
    }
}
