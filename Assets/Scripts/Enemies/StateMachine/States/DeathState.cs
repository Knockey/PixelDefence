using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
public class DeathState : MonoBehaviour
{
    [SerializeField] private float _timeUntillDeath = 1f;

    private Animator _animator;
    private BoxCollider2D _collider;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(Die());
    }

    private IEnumerator Die()
    {
        _collider.enabled = false;
        _animator.SetTrigger("Died");

        yield return new WaitForSeconds(_timeUntillDeath);

        Destroy(gameObject);
    }
}
