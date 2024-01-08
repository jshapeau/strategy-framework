using System.Collections.Generic;

public class ActionResolver
{
    private List<GameActionOutcome> outcomes;

    public ActionResolver()
    {
        this.outcomes = new List<GameActionOutcome>();
    }

    public void RegisterOutcome(GameActionOutcome outcome)
    {
        this.outcomes.Add(outcome);
        outcome.Preview();
    }

    public void Resolve()
    {
        Debug.Log("Unimplemented");
    }

    public void Cancel()
    {
        Debug.Log("Unimplemented");
    }
}
