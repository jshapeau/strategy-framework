using System;

public interface ITypedSelector<T>
{
    public T Selection { get; }
    public void Initialize(T initialSelection);
    public event Action<T> OnSelect;
}