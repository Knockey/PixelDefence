using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rollDistance;
    [SerializeField] private PlayerMovementStick _stick;

    private float _previousXPosition;
    private Vector2 _direction = Vector2.right;

    public event UnityAction<bool> Turned;
    public event UnityAction Rolled;

    private void OnEnable()
    {
        _stick.StickMovedInDirection += OnStickMoved;
    }

    private void OnDisable()
    {
        _stick.StickMovedInDirection -= OnStickMoved;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Roll();
        }
    }

    private void OnStickMoved(Vector2 direction)
    {
        _direction = direction;
        Move(_direction, _speed, 1f);
    }

    private void Roll()
    {
        Rolled?.Invoke();

        Move(_direction, _rollDistance, 3f);
    }

    private void Move(Vector2 direction, float speed, float moveTowardsStep)
    {
        _previousXPosition = transform.position.x;

        Vector3 nextPosition = new Vector3(direction.x, 0f, direction.y);
        nextPosition = (speed * Time.deltaTime * nextPosition) + transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveTowardsStep);

        Turned?.Invoke(_previousXPosition > transform.position.x);
    }
}

