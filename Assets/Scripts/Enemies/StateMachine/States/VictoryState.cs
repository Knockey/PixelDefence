using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VictoryState : State
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Victory");
    }
}
