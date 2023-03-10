using System;
using UnityEngine;

[Serializable]
public class MenuMode : ViewOperator<MenuModeView>
{
   

    public event Action SelectMarker3DEvent;
    public event Action ScreenShotEvent;

    public void Initialize()
    {
        base.CreateView();
        view.Initialize();
        view.Open();
        view.Marker3DEvent += SelectMarker3D;
        view.ScreenShotEvent += OnScreenShot;
    }

    public void ViewOpen()
    {
        view.Open();
    }

    public void ViewClose()
    {
        view.ForceClose();
    }

    private void SelectMarker3D()
    {
        SelectMarker3DEvent?.Invoke();
    }

    private void OnScreenShot()
    {
        ScreenShotEvent?.Invoke();
    }
}
