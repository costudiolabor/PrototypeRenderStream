using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CustomImageEditorView : ViewBase, IDragHandler, IDropHandler, IPointerDownHandler
{
    [SerializeField] private RawImage drawingImage;
    [SerializeField] private GameObject panelButtons;
    //[SerializeField] private Button cancelButton;
   // [SerializeField] private Button saveButton;
   // [SerializeField] private Button undoButton;

    //public event Action CancelButtonEvent;
   // public event Action SaveButtonEvent;
  //  public event Action UndoButtonEvent;

    public event Action PointerDownEvent;
    public event Action DropEvent;
    public event Action<Vector2> DragEvent;

    private void Awake()
    {
        //Init();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Init()
    {
       // panelButtons.SetActive(false);
        // cancelButton.onClick.AddListener(delegate
        // {
        //     CancelButtonEvent?.Invoke(); 
        //     panelButtons.SetActive(false);
        // });
        //
        // saveButton.onClick.AddListener(delegate
        // {
        //     SaveButtonEvent?.Invoke();
        //     panelButtons.SetActive(false);
        // });
       // undoButton.onClick.AddListener(delegate { UndoButtonEvent?.Invoke(); });
    }

   

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Click");
       // panelButtons.SetActive(true);
        PointerDownEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(drawingImage.rectTransform, eventData.position,
                null, out var rectPoint)) return;
        DragEvent?.Invoke(rectPoint);
    }

    public void OnDrop(PointerEventData eventData)
    {
        DropEvent?.Invoke();
    }

    public void SetTexture(Texture2D texture)
    {
        drawingImage.texture = texture;
    }
}