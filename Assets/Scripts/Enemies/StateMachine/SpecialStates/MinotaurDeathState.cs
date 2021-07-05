using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(SpriteRenderer))]
public class MinotaurDeathState : State
{
    [SerializeField] private float _timeUntillDeath = 1f;

    private Animator _animator;
    private BoxCollider _collider;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _collider.enabled = false;
        _animator.SetTrigger("Died");
        _spriteRenderer.flipX = !_spriteRenderer.flipX;

        if (StateSound != null)
        {
            StateSound.Play();
        }

        yield return new WaitForSeconds(_timeUntillDeath);

        Destroy(gameObject);
    }
}
