using System;

/// <summary>
/// Select SelectableMenuItem using directional controls from a Menu.
/// </summary>
public class MenuItemSelector : IMenuItemSelector {

    //PUBLIC//
    public ISelectableMenuItem Selection {get; private set; }
    public event Action<ISelectableMenuItem> OnSelect;
    
    //PRIVATE//
    private ISelectionVisual<ISelectableMenuItem> selectionVisual;
    private IDynamicMenu menu;
    private IHighlightStyle highlightStyle = null;

    public MenuItemSelector(IDynamicMenu menu, ISelectionVisual<ISelectableMenuItem> selectionVisual) 
    {
        this.menu = menu;
        this.selectionVisual = selectionVisual;
        this.menu.OnSelectableItemAdded += this.SetFirstSelectedItem;
        this.menu.OnSelectableItemRemoved += this.UpdateInvalidSelection;
    }

    //PUBLIC METHODS//
    public void Select() 
    {
        this.Selection.Select();
    }

    public void Activate()
    {

    }

    public void Initialize(ISelectableMenuItem initialSelection = null)
    {
        Debug.Log("Unimplemented");
    }

    public void Deactivate()
    {
        
    }

    public void Up() 
    {
        int currentIndex = this.menu.SelectableMenuItems.IndexOf(this.Selection);
        this.UpdateSelection(menu.SelectableMenuItems[this.CalculateModularIndex(currentIndex + 1)]);
    }

    public void Down()
    {
        int currentIndex = this.menu.SelectableMenuItems.IndexOf(this.Selection);
        this.UpdateSelection(menu.SelectableMenuItems[this.CalculateModularIndex(currentIndex + 1)]);
    }

    public void Left()
    {
        Debug.Log("Unimplemented");
    }

    public void Right()
    {
        Debug.Log("Unimplemented");
    }

    public void ShowValidSelections()
    {
        Debug.Log("Unimplemented");
    }

    public void HideValidSelections()
    {
        Debug.Log("Unimplemented");
    }


    //PRIVATE METHODS//
    private void SetFirstSelectedItem() 
    {
        if (menu.SelectableMenuItems.Count == 1) {
            this.Selection = menu.SelectableMenuItems[0];
        }
    }

    private void UpdateInvalidSelection(ISelectableMenuItem item) 
    {
        int currentIndex = this.menu.SelectableMenuItems.IndexOf(item);
        if (currentIndex == -1) {
            return;
        } else if (this.menu.SelectableMenuItems.Count > 1) {
            this.UpdateSelection(menu.SelectableMenuItems[this.CalculateModularIndex(currentIndex - 1)]);
            return;
        }

        this.Selection = null;
    }

    private int CalculateModularIndex(int n) 
    {
        return n % menu.SelectableMenuItems.Count;
    }

    private void UpdateSelection(ISelectableMenuItem item) 
    {
        this.selectionVisual.Select(item, highlightStyle);
        this.Selection = item;
    }

}