using System;
using UnityEngine;
using UnityEngine.EventSystems;



public class GraphicEditorView : ImageEditorView, IPointerDownHandler, IPointerUpHandler
{
    public event Action<Vector2> PointerUpEvent;
    
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("Down");
    }

    public void OnPointerUp(PointerEventData eventData) {
        
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingImage.rectTransform, eventData.position, null, out var rectPoint)) return;
        PointerUpEvent?.Invoke(rectPoint);
        //Debug.Log("Up  " + rectPoint);
    }
}