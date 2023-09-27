using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

namespace UI {

public class SelectableMenuItem : MonoBehaviour, ISelectableMenuItem
{
    public Action Select { get; set; }
    [SerializeField] private TextMeshProUGUI text;
    public string Text { get {
        return text.text;
    } set {
        text.text = value;
    } }

    public Transform GetTransform() {
        return this.transform;
    }


    void Start()
    {
        this.text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

}

}