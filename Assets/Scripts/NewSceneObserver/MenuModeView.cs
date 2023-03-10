using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuModeView : AnimatedView
{
    [SerializeField] private Button button3DMarker;
    [SerializeField] private Button ButtonScreenShot;

    public event Action Marker3DEvent;
    public event Action ScreenShotEvent;

    public void Initialize()
    {
        button3DMarker.onClick.AddListener(SelectMarker3D);
        ButtonScreenShot.onClick.AddListener(OnScreenShot);
    }
  
    private void SelectMarker3D()
    {
        Marker3DEvent?.Invoke();
    }

    private void OnScreenShot()
    {
        ScreenShotEvent?.Invoke();
    }
}
