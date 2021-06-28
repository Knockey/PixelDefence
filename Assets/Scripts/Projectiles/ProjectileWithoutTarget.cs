using UnityEngine;

public class ProjectileWithoutTarget : Projectile
{
    private Vector3 _direction;

    public void Init(Transform ShootPoint, int damage, Vector3 direction)
    {
        base.Init(ShootPoint, damage);
        _direction = direction;
    }

    protected override void TryMoveOrDestroy()
    {
        if (ShootPoint != null && (Vector3.Distance(ShootPoint.position, transform.position) < ShootDistance))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _direction, Speed * Time.deltaTime); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
