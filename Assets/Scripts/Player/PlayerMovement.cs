using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(BoxCollider))]
public class PlayerMovement : MonoBehaviour
{
    private const float RAYCAST_DISTANCE = 10f;

    [SerializeField] private float _speed;
    [SerializeField] private float _rollDistance;
    [SerializeField] private PlayerMovementStick _stick;
    [SerializeField] private Button _rollButton;
    [SerializeField] private LayerMask _terrainMask;

    private Player _player;
    private Vector2 _direction = Vector2.right;
    private bool _isAbleToMove;
    private BoxCollider _collider;
    private Vector3 _groundNormal;

    public event UnityAction<bool> Turned;
    public event UnityAction Rolled;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _collider = GetComponent<BoxCollider>();

        _isAbleToMove = true;
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
        _stick.StickMovedInDirection += OnStickMoved;
        _rollButton.onClick.AddListener(Roll);
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _stick.StickMovedInDirection -= OnStickMoved;
        _rollButton.onClick.RemoveListener(Roll);
    }

    private void Update()
    {
        var isHit = Physics.Raycast(_collider.bounds.center, Vector3.down, out RaycastHit hit, RAYCAST_DISTANCE, _terrainMask);
        if (isHit && _groundNormal != hit.normal)
        {
            _groundNormal = hit.normal;
        }
    }

    private void OnDied()
    {
        _isAbleToMove = false;
    }

    private void OnStickMoved(Vector2 direction)
    {
        if (_isAbleToMove)
        {
            _direction = direction;
            Move(_direction, _speed);
        }
    }

    private void Roll()
    {
        if (_isAbleToMove)
        {
            Rolled?.Invoke();            

            Move(_direction, _rollDistance);
        }
    }

    private void Move(Vector2 direction, float speed)
    {
        float previousXPosition = transform.position.x;

        Vector3 moveAlongGround = new Vector3(_groundNormal.y, -_groundNormal.x, _groundNormal.y);
        Vector3 nextPosition = new Vector3(direction.x * moveAlongGround.x, 0f * moveAlongGround.y, direction.y * moveAlongGround.z);

        nextPosition = (speed * nextPosition) + transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Turned?.Invoke(previousXPosition > transform.position.x);
    }    
}

