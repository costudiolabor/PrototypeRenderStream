using System;
using UnityEngine;

[Serializable]
public class UiController 
{
    [SerializeField] private ViewBasic viewBasic;

    public event Action ClickButtonDeleteEvent;

    public void Init(ViewBasic viewBasic)
    {
        //Debug.Log("UiController");
        this.viewBasic = viewBasic;
        this.viewBasic.ClickButtonDeleteEvent += ButtonClickDelete;
    }

    private void ButtonClickDelete()
    {
        ClickButtonDeleteEvent?.Invoke();
    }

}
