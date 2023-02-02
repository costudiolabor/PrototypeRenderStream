using System;
using UnityEngine;

[Serializable]
public class UiController 
{
    [SerializeField] private ViewBasic viewBasic;

    public event Action ClickButtonDeleteEvent;

    public UiController(ViewBasic viewBasic)
    {
        //Debug.Log("UiController");
        viewBasic.ClickButtonDeleteEvent += ButtonClickDelete;
    }

    private void ButtonClickDelete()
    {
        ClickButtonDeleteEvent?.Invoke();
    }

}
