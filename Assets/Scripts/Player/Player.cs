using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour, IApplyDamage
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Spawner _spawner;

    private int _health;
    private bool _isAlive = true;
    private bool _isVulnerable = true;
    private PlayerMovement _movement;

    public bool IsAlive => _isAlive;
    public bool IsVulnerable => _isVulnerable;

    public event UnityAction Died;
    public event UnityAction<int, int> HealthChanged;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _movement.VulnerabilityChanged += OnVulnerabilityChanged;
        _spawner.WaveFinished += OnWaveFinished;
    }

    private void OnDisable()
    {
        _movement.VulnerabilityChanged -= OnVulnerabilityChanged;
        _spawner.WaveFinished -= OnWaveFinished;
    }

    private void Start()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    private void OnVulnerabilityChanged(bool isVulnerable)
    {
        _isVulnerable = isVulnerable;
    }

    private void OnWaveFinished()
    {
        _health = _maxHealth;
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    public void ApplyDamage(int damage)
    {
        _health -= damage;
        HealthChanged?.Invoke(_health, _maxHealth);

        if (_health <= 0 && _isAlive)
        {
            _isAlive = false;
            Died?.Invoke();
        }
    }
}
