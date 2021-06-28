using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerMeleeAttack))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovementStick _movementStick;
    
    private Animator _animator;
    private Player _player;
    private PlayerMovement _movement;
    private PlayerMeleeAttack _meleeAttack;
    private SpriteRenderer _spriteRenderer;
    private bool _isTurnedRight;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
        _movement = GetComponent<PlayerMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _meleeAttack = GetComponent<PlayerMeleeAttack>();

        _isTurnedRight = true;
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
        _movementStick.StickMoved += OnStickMoved;
        _movement.Turned += OnTurned;
        _movement.Rolled += OnRolled;
        _meleeAttack.Attacked += OnAttacked;
    }    

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _movementStick.StickMoved -= OnStickMoved;
        _movement.Turned -= OnTurned;
        _movement.Rolled -= OnRolled;
        _meleeAttack.Attacked -= OnAttacked;
    }

    private void OnTurned(bool state)
    {
        if (_isTurnedRight != state)
        {
            _isTurnedRight = state;
            _spriteRenderer.flipX = _isTurnedRight;
        }
    }

    private void OnStickMoved(bool state)
    {
        _animator.SetBool("IsMoving", state);
    }

    private void OnAttacked()
    {
        _animator.Play("MeleeAttack");
    }

    private void OnRolled()
    {
        _animator.Play("Roll");
    }

    private void OnDied()
    {
        _animator.Play("Death");
    }
}
