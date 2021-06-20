public class TransitionByTargetDeath : Transition
{
    private void Update()
    {
        if (!Target.IsAlive)
            NeedTransit = true;
    }
}
