using System;

public interface ICommand
{
    public IDirectionalSelector Selector { get; }
    public Action<ActionData> OnExecute { get; set; }
    public void Initialize();
}
