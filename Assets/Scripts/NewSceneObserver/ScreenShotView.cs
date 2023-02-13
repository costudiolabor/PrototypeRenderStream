using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ScreenShotView : AnimatedView,  IPointerDownHandler
{
    public event Action PointerDownEvent;
    
    public void OnPointerDown(PointerEventData eventData) {
        PointerDownEvent?.Invoke();
    }
}
