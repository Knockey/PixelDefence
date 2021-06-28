using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float Speed;
    [SerializeField] protected float ShootDistance;

    protected Transform ShootPoint;
    protected int Damage;

    private void Update()
    {
        TryMoveOrDestroy();
    }

    private void OnTriggerEnter(Collider other)
    {
        ProjectileCollided(other);
    }

    protected void ProjectileCollided(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            if (player.IsVulnerable)
            {
                player.ApplyDamage(Damage);
            }

            Destroy(gameObject);
        }
    }

    protected abstract void TryMoveOrDestroy();

    public void Init(Transform shootPoint, int damage)
    {
        Damage = damage;
        ShootPoint = shootPoint;
    }
}
