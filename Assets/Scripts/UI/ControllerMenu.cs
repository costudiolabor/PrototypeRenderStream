using System;
using UnityEngine;

public class ControllerMenu : MonoBehaviour
{
    [SerializeField] private ViewMenu _viewMenu;

    private Camera _mainCamera;

    public event Action<Color> ChangeColorEvent;

    public void Init(Camera mainCamera)
    {
        _mainCamera = mainCamera;

        _viewMenu.MicOnEvent += MicOn;
        _viewMenu.MicOffEvent += MicOff;
        _viewMenu.CameraOnEvent += CameraOn;
        _viewMenu.CameraOffEvent += CameraOff;
        _viewMenu.ChangeColorEvent += color => ChangeColorEvent?.Invoke(color);
    }

    private void MicOn()
    {
    }

    private void MicOff()
    {
    }

    private void CameraOn()
    {
    }

    private void CameraOff()
    {
    }
}