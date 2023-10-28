using System;

public interface ICommand
{
    public IDirectionalSelector Selector { get; }
    public Action<IGameAction> OnExecute { get; set; }
}