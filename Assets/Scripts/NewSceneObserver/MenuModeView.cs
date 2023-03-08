using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuModeView : AnimatedView, IPointerDownHandler
{
    [SerializeField] private Button button3DMarker;
    [SerializeField] private Button ButtonScreenShot;

    public event Action Marker3DEvent;
    public event Action ScreenShotEvent;
    public event Action<Vector2> PointerDownEvent;

    public void Initialize()
    {
        button3DMarker.onClick.AddListener(SelectMarker3D);
        ButtonScreenShot.onClick.AddListener(OnScreenShot);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("SetMarker: " + eventData.position);
        PointerDownEvent?.Invoke(eventData.position);
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
