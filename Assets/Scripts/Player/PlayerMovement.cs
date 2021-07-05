using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rollDistance;
    [SerializeField] private float _invulnerabilityTimeInRoll;
    [SerializeField] private PlayerMovementStick _stick;
    [SerializeField] private Button _rollButton;

    private Player _player;
    private Vector2 _direction = Vector2.right;
    private Coroutine _rollCoroutine;
    private bool _isAbleToMove;

    public event UnityAction<bool> Turned;
    public event UnityAction Rolled;
    public event UnityAction<bool> VulnerabilityChanged;

    private void Awake()
    {
        _player = GetComponent<Player>();
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

    private void OnDied()
    {
        _isAbleToMove = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isAbleToMove)
        {
            Roll();
        }
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

            if (_rollCoroutine != null)
            {
                StopCoroutine(_rollCoroutine);
            }

            _rollCoroutine = StartCoroutine(ChangeVulnerability());

            Move(_direction, _rollDistance);
        }
    }

    private void Move(Vector2 direction, float speed)
    {
        float previousXPosition = transform.position.x;

        Vector3 nextPosition = new Vector3(direction.x, 0f, direction.y);
        nextPosition = (speed * nextPosition) + transform.position;
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Turned?.Invoke(previousXPosition > transform.position.x);
    }

    private IEnumerator ChangeVulnerability()
    {
        VulnerabilityChanged?.Invoke(false);

        yield return new WaitForSeconds(_invulnerabilityTimeInRoll);

        VulnerabilityChanged?.Invoke(true);
    }
}

