public interface ISelectionVisual<T>
{
    public void Select(T selection, IHighlightStyle style);
    public void Deselect();
    public void Activate();
    public void Deactivate();
}