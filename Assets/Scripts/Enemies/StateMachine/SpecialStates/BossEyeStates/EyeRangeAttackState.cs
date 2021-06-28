using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EyeRangeAttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private ProjectileWithoutTarget _projectile;
    [SerializeField] private EyeMoveState _movement;
    [SerializeField] private float _heightOverGround;
    [SerializeField] private int _projectilesCount;
    [SerializeField] private float _delayAfterAttack;

    private Animator _animator;

    public event UnityAction AttackFinished;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Attack();
    }

    private void Attack()
    {
        _animator.Play("RangeAttack");

        Vector3 shootPoint = transform.position;
        shootPoint.y = shootPoint.y - _movement.FlightHeight + _heightOverGround;

        float stepAngle = 360f / _projectilesCount;

        for (int i = 0; i < _projectilesCount; i++)
        {
            var projectile = Instantiate(_projectile, shootPoint, Quaternion.identity);

            float newRotationAngle = stepAngle * i - 90f;
            Vector3 direction = new Vector3(Mathf.Sin(stepAngle * i * Mathf.Deg2Rad), 0, Mathf.Cos(stepAngle * i * Mathf.Deg2Rad));

            projectile.transform.rotation = Quaternion.Euler(0f, newRotationAngle, 0f);
            projectile.Init(transform, _damage, direction);
        }

        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delayAfterAttack);

        AttackFinished?.Invoke();
    }
}
