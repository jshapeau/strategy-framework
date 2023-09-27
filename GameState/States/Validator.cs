using System.Collections;
using System.Collections.Generic;

namespace GameState {

public class Validator : CombatDecorator
{
    public override GameStateType StateType { get; } = GameStateType.Validator;
    public override IDirectionalSelector Selector => this.selector;
    
    private IDirectionalSelector selector;

    public Validator(Combat baseState)
        :base(baseState)
    {

    }

    public void Activate(ICommand command, IGameState parent = null) 
    {
        this.PreviousState = parent;
        
        this.selector = command.Selector;
        // this.selector.Initialize(command.);
    
        this.selector.ShowValidSelections();
    
        this.OnStateEntered?.Invoke();
    }

    public override void Deactivate() 
    {   
        this.selector.Deactivate();
        this.selector = null;
        base.Deactivate();
    }
}

}