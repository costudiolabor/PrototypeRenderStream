using System;
using UnityEngine;
using UnityEngine.EventSystems;

using  UnityEngine.UI;


public class ImageEditorView : AnimatedView, IDragHandler, IDropHandler {
    [SerializeField] private RawImage drawingImage;
    [SerializeField] private Toggle controlsHideToggle;
    [SerializeField] private Tools drawTools;
    public RectTransform linesParent;
    public event Action DropEvent;
    public event Action<Vector2> DragEvent;
    

    public Tools tools => drawTools;

    public void OnDrag(PointerEventData eventData){
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingImage.rectTransform, eventData.position, null, out var rectPoint)) return;
        DragEvent?.Invoke(rectPoint);
    }

    public void OnDrop(PointerEventData eventData){
        DropEvent?.Invoke();
    }

    public void SetTexture(Texture2D texture){
        drawingImage.texture = texture;
    }
}