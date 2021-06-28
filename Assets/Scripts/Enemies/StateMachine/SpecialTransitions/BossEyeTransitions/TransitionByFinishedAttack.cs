using UnityEngine;

public class TransitionByFinishedAttack : Transition
{
    [SerializeField] private EyeRangeAttackState _attackState;

    private void OnEnable()
    {
        _attackState.AttackFinished += OnAttackFinished;
    }

    private void OnDisable()
    {
        _attackState.AttackFinished -= OnAttackFinished;
    }

    private void OnAttackFinished()
    {
        NeedTransit = true;
    }
}
