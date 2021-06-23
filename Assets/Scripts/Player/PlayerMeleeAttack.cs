using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private AttackZone _attackZone;
    [SerializeField] private Button _attackButton;

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
        _attackButton.onClick.AddListener(Attack);
    }


    private void OnDisable()
    {
        _movement.Turned -= OnTurned;
        _attackButton.onClick.RemoveListener(Attack);
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
            enemy.ApplyDamage(_damage);
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
