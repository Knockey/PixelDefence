using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class MoveState : State
{
    [SerializeField] private float _speed;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isTurnedRight;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _animator.Play("Run");

        if (StateSound != null)
        {
            StateSound.Play();
        }
    }

    private void Update()
    {
        float previousXPosition = transform.position.x;
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);

        TryTurnRight(previousXPosition > transform.position.x);
    }

    private void TryTurnRight(bool isTurnedRight)
    {
        if (isTurnedRight != _isTurnedRight)
        {
            _isTurnedRight = isTurnedRight;
            _spriteRenderer.flipX = _isTurnedRight;
        }
    }
}
