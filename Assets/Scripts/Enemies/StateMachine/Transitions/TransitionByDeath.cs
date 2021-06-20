using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class TransitionByDeath : Transition
{
    private Enemy _character;

    private void Awake()
    {
        _character = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _character.Died += OnDied;
    }

    private void OnDisable()
    {
        _character.Died -= OnDied;
    }

    private void OnDied(Enemy enemy)
    {
        NeedTransit = true;
    }
}
