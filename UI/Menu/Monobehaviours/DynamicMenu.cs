using System;
using System.Collections.Generic;

namespace UI {

public abstract class DynamicMenu : Menu, IDynamicMenu {
    
    [SerializeField] private Transform grid;
    [SerializeField] private SelectableMenuItem selectablePrefab;
    public Action OnSelectableItemAdded { get; set; }
    public Action<ISelectableMenuItem> OnSelectableItemRemoved { get; set; }

    protected override void Start() {
       if (selectablePrefab == null) {
            throw new Exception("SelectableMenuItem instance is null.");
       }

       base.Start();
    }

    public virtual void AddSelectableItem(string name, Action action) {
        SelectableMenuItem selectableMenuItem = GameObject.Instantiate(this.selectablePrefab);
        selectableMenuItem.Select = action;
        selectableMenuItem.Text = name;
        selectableMenuItem.transform.SetParent(this.grid, false);
        this.SelectableMenuItems.Add(selectableMenuItem);

        this.OnSelectableItemAdded?.Invoke();
    }

    public virtual void RemoveSelectableItem(ISelectableMenuItem item){
        this.OnSelectableItemRemoved.Invoke(item);
        this.SelectableMenuItems.RemoveAt(this.SelectableMenuItems.IndexOf(item));
    }

    public override void Close() {
        this.ClearSelectableItems();
        base.Close();
    }

    public virtual void ClearSelectableItems() {
        foreach (SelectableMenuItem item in this.SelectableMenuItems) {
            GameObject.Destroy(item.gameObject);
        }
        this.SelectableMenuItems.Clear();
    }

}

}