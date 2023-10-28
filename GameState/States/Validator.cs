using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameState {

public class Validator : CombatDecorator
{
    public override GameStateType StateType { get; } = GameStateType.Validator;
    public override IDirectionalSelector Selector => this.selector;
    
    private IDirectionalSelector selector;
    private ICommand command;

    public Validator(Combat baseState)
        :base(baseState)
    {

    }

    public void Activate(ICommand command, IGameState parent = null) 
    {
        this.PreviousState = parent;
        this.command = command;
        this.selector = command.Selector;
        this.selector.ShowValidSelections();

        this.command.OnExecute += SwitchToGameActionState;
        this.OnStateEntered?.Invoke();
    }

    private void SwitchToGameActionState(IGameAction action)
    {
        Debug.Log("Unimplemented");
    }

    public override void Deactivate() 
    {   
        this.selector.Deactivate();
        this.command.OnExecute += SwitchToGameActionState;
        this.selector = null;
        base.Deactivate();
    }
}

}