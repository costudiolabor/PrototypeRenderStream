using System;
using UnityEngine;

public class ControllerMainMenu : MonoBehaviour
{
    [SerializeField] private ViewMainMenu viewMainMenu;
    [SerializeField] private ControllerMenu controllerMenu;

    public event Action<Color> ChangeColorEvent;

    public void Init(Camera mainCamera)
    {
        viewMainMenu.Init();
        controllerMenu.Init(mainCamera);
        viewMainMenu.MenuEvent += isActive => controllerMenu.gameObject.SetActive(isActive);
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