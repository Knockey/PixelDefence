using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EyeMoveState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _flightHeight;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _isTurnedRight;

    public float FlightHeight => _flightHeight;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        Vector3 initialPosition = transform.position;
        initialPosition.y += _flightHeight;
        transform.position = initialPosition;
    }

    private void OnEnable()
    {
        _animator.Play("Flight");

        if (StateSound != null)
        {
            StateSound.Play();
        }
    }

    private void Update()
    {
        float previousXPosition = transform.position.x;
        Vector3 destination = Target.transform.position;
        destination.y += _flightHeight;

        transform.position = Vector3.MoveTowards(transform.position, destination, _speed * Time.deltaTime);

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
