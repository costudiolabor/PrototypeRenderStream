using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class UISwipeDelete : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private float offsetDelete;
    private Vector2 _offSet;
    private Vector2 _startPosition;
    private RectTransform _transformGallery;
    private Transform _lastParent;
    
    public event Action DestroyImageEvent;
    
    public void SetGallery(RectTransform transformGallery) {
        _transformGallery = transformGallery;
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        _lastParent = gameObject.transform.parent;
        gameObject.transform.SetParent(_transformGallery);
        _startPosition = rectTransform.position;
        _offSet = eventData.pointerCurrentRaycast.screenPosition - new Vector2(_startPosition.x, _startPosition.y);
    }

    public void OnDrag(PointerEventData eventData) {
        Vector3 position = eventData.pointerCurrentRaycast.screenPosition - _offSet;
        position = new Vector2(_startPosition.x, position.y);
        rectTransform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (rectTransform.position.y - _startPosition.y > offsetDelete) {
            DestroyImageEvent?.Invoke();
        }
        else {
            rectTransform.position = _startPosition;
            gameObject.transform.SetParent(_lastParent);
        }
    }
}
