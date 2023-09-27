using System.Collections.Generic;

/// <summary>
/// Provides basic methods for menus.
/// </summary>
public interface IMenu 
{
    public List<ISelectableMenuItem> SelectableMenuItems { get; }
    public void Open();
    public void Close();
}