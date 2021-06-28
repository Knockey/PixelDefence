using UnityEngine;

public class TransitionByTimeOut : Transition
{
    [SerializeField] private float _timeBeforeTransition;

    private float _timeLeft;

    private void Awake()
    {
        _timeLeft = _timeBeforeTransition;
    }

    private void Update()
    {
        _timeLeft -= Time.deltaTime;

        if (_timeLeft <= 0)
        {
            _timeLeft = _timeBeforeTransition;
            NeedTransit = true;
        }
    }
}
