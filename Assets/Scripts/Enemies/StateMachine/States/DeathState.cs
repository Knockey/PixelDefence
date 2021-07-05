using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
public class DeathState : State
{
    [SerializeField] private float _timeUntillDeath = 1f;

    private Animator _animator;
    private BoxCollider _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _collider.enabled = false;
        _animator.SetTrigger("Died");

        if (StateSound != null)
        {
            StateSound.Play();
        }

        yield return new WaitForSeconds(_timeUntillDeath);

        Destroy(gameObject);
    }
}
