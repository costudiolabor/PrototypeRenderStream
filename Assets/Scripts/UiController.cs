using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UiController 
{
    [SerializeField] private View _view;

    public event Action ClickButtonDeleteEvent;

    public UiController(View _view)
    {
        _view.ClickButtonDeleteEvent += ButtonClickDelete;
    }

    private void ButtonClickDelete()
    {
        ClickButtonDeleteEvent?.Invoke();
    }

}
