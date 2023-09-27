using System.Collections;
using System.Collections.Generic;
using System;

public abstract class BaseGameAction : IGameAction
{
    public IGameAction Parent { get; set; }
    protected List<IGameAction> children;
    protected List<GameActionOutcome> actionOutcomes = new List<GameActionOutcome>();
    [SerializeField] protected ActionQueue actionQueue;

    public List<IGameAction> GetChildren()
    {
        return this.children;
    } 

    public List<GameActionOutcome> GetOutcomes()
    {
        return this.actionOutcomes;
    }

    public void RegisterOutcome(Action outcome)
    {
        GameActionOutcome gameActionOutcome = new GameActionOutcome(outcome, this);
        actionOutcomes.Add(gameActionOutcome);
        actionQueue.RegisterOutcome(gameActionOutcome);
    }

    private bool HasChildren()
    {
        if (children.Count > 0)
        {
            return true;
        }
        return false;
    }    

    public virtual void Execute()
    {

    }

    public virtual void Execute(List<GridPosition> targetPosition)
    {

    }

    public virtual void Execute(IUnit target)
    {

    }
}
