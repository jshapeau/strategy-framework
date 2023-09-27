using System;

/// <summary>
/// Methods for menus pertaining to dynamically changing selectable content.
/// </summary>
public interface IDynamicMenu : IMenu 
{
    public Action OnSelectableItemAdded { get; set; }
    public Action<ISelectableMenuItem> OnSelectableItemRemoved { get; set; }
    public void AddSelectableItem(string Name, Action action);
    public void RemoveSelectableItem(ISelectableMenuItem item);
    public void ClearSelectableItems();
}