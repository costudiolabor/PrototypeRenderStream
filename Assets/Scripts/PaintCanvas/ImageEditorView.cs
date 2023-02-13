using System;
using UnityEngine;
using UnityEngine.Events;
using  UnityEngine.UI;

public class ImageEditorView : AnimatedView {
    [SerializeField] private RectTransform toolsParent;
    [SerializeField] private Toggle lineToggle, arrowToggle;
    [SerializeField] private Button acceptButton, cancelButton, undoButton; 

    public ColorMenu colorMenu;
    public DrawingCanvas drawingCanvas;

    public RectTransform tools => toolsParent;
    public event Action AcceptClickedEvent, RejectClickedEvent, UndoEvent, LineSelectedEvent, ArrowSelectedEvent;
    
    private void OnLineSelect(bool isOn){
        if (!isOn) return;
        LineSelectedEvent?.Invoke();
    }

    private void OnArrowSelect(bool isOn){
         if (!isOn) return;
        ArrowSelectedEvent?.Invoke();
    }
    
    public virtual void NotifySelectedTool(){
        if (lineToggle.isOn) LineSelectedEvent?.Invoke();
        if (arrowToggle.isOn) ArrowSelectedEvent?.Invoke();
    }

    public virtual  void Awake(){
        acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
        cancelButton.onClick.AddListener(delegate { RejectClickedEvent?.Invoke(); });
        undoButton.onClick.AddListener(delegate { UndoEvent?.Invoke(); });
        lineToggle.onValueChanged.AddListener(OnLineSelect);
        arrowToggle.onValueChanged.AddListener(OnArrowSelect);
    }

    private void OnDestroy(){
        acceptButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
        lineToggle.onValueChanged.RemoveAllListeners();
        arrowToggle.onValueChanged.RemoveAllListeners();
    }
}