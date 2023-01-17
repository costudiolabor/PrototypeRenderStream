using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UiController 
{
    [SerializeField] private View view;

    public event Action ClickButtonDeleteEvent;

    public UiController(View view)
    {
        Debug.Log("UiController");
        view.ClickButtonDeleteEvent += ButtonClickDelete;
    }

    private void ButtonClickDelete()
    {
        ClickButtonDeleteEvent?.Invoke();
    }

}
