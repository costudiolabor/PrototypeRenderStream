using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class UISwipeDelete : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float offsetDelete;
    private Vector2 _offSet;
    private Vector2 _startPosition;

    public void OnBeginDrag(PointerEventData eventData) {
        _startPosition = rectTransform.position;
        _offSet = eventData.pointerCurrentRaycast.screenPosition - new Vector2(_startPosition.x, _startPosition.y);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 position = eventData.pointerCurrentRaycast.screenPosition - _offSet;
        position = new Vector2(_startPosition.x, position.y);
        rectTransform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (rectTransform.position.y - _startPosition.y > offsetDelete) 
            Destroy(this.gameObject);
        else 
            rectTransform.position = _startPosition;
    }
}
