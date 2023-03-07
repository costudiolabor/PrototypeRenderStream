using System;
using UnityEngine;

public class HidePanel : AnimatedView {
    protected Action onHideEvent;
    private void Update(){
        if (!Input.GetMouseButtonDown(0)) return;
        var position = (Vector2)Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, position)) return;
        Close();
        onHideEvent?.Invoke();
    }
}