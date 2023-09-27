public interface IValidator<T> {
    public bool Validate(T item);
    public T GetValidItems();
}