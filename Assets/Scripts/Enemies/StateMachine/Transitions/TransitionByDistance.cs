using UnityEngine;

public class TransitionByDistance : Transition
{
    [SerializeField] private float _distance;
    [SerializeField] private float _dispersion;

    private float _transitionDistance;

    private void Awake()
    {
        _transitionDistance = _distance + Random.Range(-_dispersion, _dispersion);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, Target.transform.position) < _transitionDistance)
        {
            NeedTransit = true;
        }
    }
}
