using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Fireball : Projectile
{
    [SerializeField] private float _heightOffset;

    private Player _target;
    private bool _isTurnedRight;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Transform shootPosition, int damage, Player target)
    {
        base.Init(shootPosition, damage);
        _target = target;
    }

    protected override void TryMoveOrDestroy()
    {
        if ((Vector3.Distance(transform.position, ShootPoint.position) < ShootDistance) && _target != null)
        {
            float previousXPosition = transform.position.x;

            Vector3 newPosition = _target.transform.position;
            newPosition.y += _heightOffset;

            transform.position = Vector3.MoveTowards(transform.position, newPosition, Speed * Time.deltaTime);

            TryTurnRight(previousXPosition > transform.position.x);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void TryTurnRight(bool isTurnedRight)
    {
        if (isTurnedRight != _isTurnedRight)
        {
            _isTurnedRight = isTurnedRight;
            _spriteRenderer.flipX = _isTurnedRight;
        }
    }
}
