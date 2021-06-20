using UnityEngine;

public class Player : MonoBehaviour
{
    private bool _isAlive = true;

    public bool IsAlive => _isAlive;

    public void ApplyDamage(int damage)
    {
        Debug.Log("Hit");
    }

}
