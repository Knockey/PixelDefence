using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rollDistance;
    [SerializeField] private PlayerMovementStick _stick;
    [SerializeField] private Button _rollButton;

    private Vector2 _direction = Vector2.right;

    public event UnityAction<bool> Turned;
    public event UnityAction Rolled;

    private void OnEnable()
    {
        _stick.StickMovedInDirection += OnStickMoved;
        _rollButton.onClick.AddListener(Roll);
    }

    private void OnDisable()
    {
        _stick.StickMovedInDirection -= OnStickMoved;
        _rollButton.onClick.RemoveListener(Roll);
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
        Move(_direction, _speed);
    }

    private void Roll()
    {
        Rolled?.Invoke();

        Move(_direction, _rollDistance);
    }

    private void Move(Vector2 direction, float speed)
    {
        float previousXPosition = transform.position.x;

        Vector3 nextPosition = new Vector3(direction.x, 0f, direction.y);
        nextPosition = (speed * nextPosition) + transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Turned?.Invoke(previousXPosition > transform.position.x);
    }
}

