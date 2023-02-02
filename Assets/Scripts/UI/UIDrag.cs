using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IDragHandler //, IBeginDragHandler, IEndDragHandler
{
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = eventData.pointerCurrentRaycast.screenPosition;
        Debug.Log("On Drag!");

    }

    // public void OnBeginDrag(PointerEventData eventData)
    // {
    //    Debug.Log("You dragging!");
    // }
    //
    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     Debug.Log("Drag me!");
    // }
}