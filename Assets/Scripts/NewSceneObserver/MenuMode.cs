using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MenuMode : ViewOperator<MenuModeView>
{
    [SerializeField] private MarkerEditor markerEditor;

    public event Action Marker3DEvent;
    public event Action ScreenShotEvent;

    public void Initialize()
    {
        base.CreateView();
        view.Initialize();
        view.Open();
        view.Marker3DEvent += SelectMarker3D;
        view.ScreenShotEvent += OnScreenShot;

        markerEditor.Initialize();
        view.PointerDownEvent += OnPointerDown;
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
        Marker3DEvent?.Invoke();
    }

    private void OnScreenShot()
    {
        ScreenShotEvent?.Invoke();
    }

    public void OnPointerDown(Vector2 position)
    {
        Debug.Log("SetMarker: " + position);
        markerEditor.CreateSticker(position);
    }

    public void SetColor(Color color)
    {
        markerEditor.SetColor(color);
    }

    public void Clear()
    {
        markerEditor.Clear();
    }

   

}
