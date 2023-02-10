using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : ViewBase, IDragHandler, IBeginDragHandler//, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    private Vector2 offSet;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var position = rectTransform.position;
        offSet = eventData.pointerCurrentRaycast.screenPosition - new Vector2(position.x, position.y);
    }

    public void OnDrag(PointerEventData eventData) {
        
        rectTransform.position = eventData.pointerCurrentRaycast.screenPosition - offSet;
    }

   
}