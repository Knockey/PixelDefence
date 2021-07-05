using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private AttackZone _attackZone;
    [SerializeField] private Button _attackButton;
    [SerializeField] private float _attackDelay = 0.35f;

    private Player _player;
    private PlayerMovement _movement;
    private bool _isAbleToAttack;
    private bool _isTurnedRight;

    public event UnityAction Attacked;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _movement = GetComponent<PlayerMovement>();

        _isAbleToAttack = true;
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
        _movement.Turned += OnTurned;
        _attackButton.onClick.AddListener(Attack);
    }
    private void OnDisable()
    {
        _player.Died -= OnDied;
        _movement.Turned -= OnTurned;
        _attackButton.onClick.RemoveListener(Attack);
    }

    private void OnDied()
    {
        _isAbleToAttack = false;
    }

    private void Attack()
    {
        if (_isAbleToAttack)
        {
            Attacked?.Invoke();
            foreach (var enemy in _attackZone.GetEnemiesInRange())
            {
                enemy.ApplyDamage(_damage);
            }

            StartCoroutine(StartDelayTimer());
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

    private IEnumerator StartDelayTimer()
    {
        _isAbleToAttack = false;

        yield return new WaitForSeconds(_attackDelay);

        _isAbleToAttack = true;
    }
}
