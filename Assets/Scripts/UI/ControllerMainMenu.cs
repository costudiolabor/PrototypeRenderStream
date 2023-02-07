using System;
using UnityEngine;

public class ControllerMainMenu : ViewBase
{
    [SerializeField] private ViewMainMenu viewMainMenu;
    [SerializeField] private ControllerMenu controllerMenu;

    public event Action<Color> ChangeColorEvent;
    
    public event Action
        MicOnEvent,
        MicOffEvent,
        CameraOnEvent,
        CameraOffEvent;

    public void Init(Camera mainCamera)
    {
        viewMainMenu.Init();
        controllerMenu.Init();
        viewMainMenu.MenuEvent += isActive => controllerMenu.gameObject.SetActive(isActive);
        controllerMenu.MicOnEvent += () => MicOnEvent?.Invoke(); 
        controllerMenu.MicOffEvent += () => MicOffEvent?.Invoke();  
        controllerMenu.CameraOnEvent += () => CameraOnEvent?.Invoke();
        controllerMenu.CameraOffEvent += () =>  CameraOffEvent?.Invoke(); 
        controllerMenu.ChangeColorEvent += ChangeColor;
    }

    private void ChangeColor(Color color)
    {
        ChangeColorEvent?.Invoke(color);
        viewMainMenu.CloseMenu();
    }
    
    private void OnDisable()
    {
        viewMainMenu.MenuEvent -= controllerMenu.gameObject.SetActive;
    }
}