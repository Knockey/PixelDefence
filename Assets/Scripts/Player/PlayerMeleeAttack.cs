using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private AttackZone _attackZone;

    private PlayerMovement _movement;
    private bool _isTurnedRight;

    public event UnityAction Attacked;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.Turned += OnTurned;
    }


    private void OnDisable()
    {
        _movement.Turned -= OnTurned;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Attacked?.Invoke();
        foreach (var enemy in _attackZone.GetEnemiesInRange())
        {
            Debug.Log(enemy.name);
        }
    }

    private void OnTurned(bool state)
    {
        if (_isTurnedRight != state)
        {
            Vector3 newAttackZonePosition = _attackZone.transform.localPosition;
            newAttackZonePosition.x *= -1;

            _attackZone.transform.localPosition = newAttackZonePosition;
            _isTurnedRight = state;
        }
    }
}
