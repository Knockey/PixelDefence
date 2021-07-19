using UnityEngine;

public class TransitionByTargetOutOfRange : Transition
{
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _dispersion;

    private float _transitionDistance;

    private void Awake()
    {
        _transitionDistance = _attackDistance + Random.Range(-_dispersion, _dispersion);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) > _transitionDistance)
        {
            NeedTransit = true;
        }
    }
}
