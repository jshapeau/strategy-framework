using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

public abstract class Menu : MonoBehaviour, IMenu {
    public List<ISelectableMenuItem> SelectableMenuItems { get; private set; }
    // public IMenuItemSelector Selector { get; private set; }

    protected virtual void Start() {
        this.SelectableMenuItems = new List<ISelectableMenuItem>();
        gameObject.SetActive(false);
    }

    public virtual void Open() {
        this.gameObject.SetActive(true);
    }
    public virtual void Close() {
        this.gameObject.SetActive(false);
    }
}

}