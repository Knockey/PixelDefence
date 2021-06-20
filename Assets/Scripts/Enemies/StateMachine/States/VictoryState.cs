using UnityEngine;

[RequireComponent(typeof(Animator))]
public class VictoryState : MonoBehaviour
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
