using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMeleeAttack))]
public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private PlayerMovementStick _movementStick;
    [SerializeField] private AudioSource _attackSound;
    [SerializeField] private AudioSource _moveSound;
    [SerializeField] private AudioSource _deathSound;

    private AudioSource _currentSound;
    private Player _player;
    private PlayerMeleeAttack _meleeAttack;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _meleeAttack = GetComponent<PlayerMeleeAttack>();
    }

    private void OnEnable()
    {
        _player.Died += () => TryChangeSound(_deathSound);
        _movementStick.StickMoved += OnStickMoved;
        _meleeAttack.Attacked += () => TryChangeSound(_attackSound);
    }

    private void OnDisable()
    {
        _player.Died -= () => TryChangeSound(_deathSound);
        _movementStick.StickMoved -= OnStickMoved;
        _meleeAttack.Attacked -= () => TryChangeSound(_attackSound);
    }

    private void TryChangeSound(AudioSource sound)
    {
        if (_currentSound != null)
        {
            if (_currentSound != sound)
            {
                _currentSound.Stop();
            }

            _currentSound = sound;
            _currentSound.Play();
        }
        else
        {
            _currentSound = sound;
            _currentSound.Play();
        }
    }

    private void OnStickMoved(bool isMoving)
    {
        if (isMoving)
        {
            TryChangeSound(_moveSound);
        }
        else
        {
            _currentSound.Stop();
            _currentSound = null;
        }
    }
}
