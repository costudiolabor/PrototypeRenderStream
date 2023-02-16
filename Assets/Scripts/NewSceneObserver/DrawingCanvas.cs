using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawingCanvas : MonoBehaviour,  IPointerDownHandler, IPointerUpHandler, IDragHandler, IDropHandler
{
    [SerializeField] protected RawImage backgroundImage;
    [SerializeField] protected RectTransform linesParent;
    public event Action<Vector2> DragEvent, PointerUpEvent,PointerDownEvent;
    public event Action DropEvent;

    public RectTransform instaceParent => linesParent;
    
    public Texture backgroundTexture{
        set => backgroundImage.texture = value;
    }
    
    public void OnDrag(PointerEventData eventData){
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, null, out var rectPoint)) return;
        DragEvent?.Invoke(rectPoint);
    }

    public void OnDrop(PointerEventData eventData){
        DropEvent?.Invoke();
    }
    
    public void OnPointerDown(PointerEventData eventData) {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, null, out var rectPoint)) return;
        PointerDownEvent?.Invoke(rectPoint);
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(backgroundImage.rectTransform, eventData.position, null, out var rectPoint)) return;
        PointerUpEvent?.Invoke(rectPoint);
    }
}