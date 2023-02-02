using System;
using UnityEngine;
using UnityEngine.UI;

public class ViewMainMenu : ViewBase
{
    [SerializeField] Button
        buttonMenuOn,
        buttonMenuOff;

    public event Action<bool> MenuEvent;


    public void Init()
    {
        buttonMenuOn.onClick.AddListener(OpenMenu);
        buttonMenuOff.onClick.AddListener(CloseMenu);
        
        buttonMenuOn.gameObject.SetActive(true);
        buttonMenuOff.gameObject.SetActive(false);
    }
       
    public void OpenMenu()
    {
        buttonMenuOn.gameObject.SetActive(false);
        buttonMenuOff.gameObject.SetActive(true);
        MenuEvent?.Invoke(true);
    }

    public void CloseMenu()
    {
        buttonMenuOn.gameObject.SetActive(true);
        buttonMenuOff.gameObject.SetActive(false);
        MenuEvent?.Invoke(false);
    }
}