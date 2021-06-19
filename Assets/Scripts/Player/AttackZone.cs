using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private List<Enemy> _enemiesInRange = new List<Enemy>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemiesInRange.Add(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            _enemiesInRange.Remove(enemy);
        }
    }

    public List<Enemy> GetEnemiesInRange()
    {
        return _enemiesInRange;
    }
}
