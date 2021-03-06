using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;    
    [SerializeField] protected AudioSource StateSound;

    protected Player Target { get; set; }

    public void Enter(Player target)
    {
        if (!enabled)
        {
            enabled = true;

            Target = target;

            foreach (var transition in _transitions)
            {
                transition.enabled = true;
                transition.Init(Target);
            }
        }
    }

    public State GetNext()
    {
        foreach (var transition in _transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    public void Exit()
    {
        if (enabled)
        {
            if (StateSound != null)
            {
                StateSound.Stop();
            }

            foreach (var transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }
}
