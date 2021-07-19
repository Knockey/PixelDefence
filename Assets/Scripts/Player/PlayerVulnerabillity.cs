using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerVulnerabillity : MonoBehaviour
{
    [SerializeField] private float _invulnerabilityTimeInRoll;

    private Coroutine _playerVulnerability;
    private PlayerMovement _movement;

    public event UnityAction<bool> VulnerabilityChanged;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.Rolled += OnRolled;
    }

    private void OnDisable()
    {
        _movement.Rolled -= OnRolled;        
    }

    private void OnRolled()
    {
        if (_playerVulnerability != null)
        {
            StopCoroutine(_playerVulnerability);
        }

        _playerVulnerability = StartCoroutine(ChangeVulnerability());
    }

    private IEnumerator ChangeVulnerability()
    {
        VulnerabilityChanged?.Invoke(false);

        yield return new WaitForSeconds(_invulnerabilityTimeInRoll);

        VulnerabilityChanged?.Invoke(true);
    }
}
