using System;
using UnityEngine;
using UnityEngine.Events;
using  UnityEngine.UI;

public class ImageEditorView : AnimatedView {
    [SerializeField] private RectTransform toolsParent;
    [SerializeField] private Toggle lineToggle, arrowToggle, stickerToggle;
    [SerializeField] private Button acceptButton, cancelButton, undoButton; 

    public ColorMenu colorMenu;
    public DrawingCanvas drawingCanvas;

    public event Action AcceptClickedEvent, RejectClickedEvent, UndoEvent, LineSelectedEvent, ArrowSelectedEvent, StickerSelectedEvent;
    
    private void OnLineSelect(bool isOn){
        if (!isOn) return;
        LineSelectedEvent?.Invoke();
    }

    private void OnArrowSelect(bool isOn){
         if (!isOn) return;
        ArrowSelectedEvent?.Invoke();
    }

    private void OnStickerSelect(bool isOn){
        if (!isOn) return;
        StickerSelectedEvent?.Invoke();
    }
    
    
    public void NotifySelectedTool(){
        if (lineToggle.isOn) LineSelectedEvent?.Invoke();
        if (arrowToggle.isOn) ArrowSelectedEvent?.Invoke();
        if (stickerToggle.isOn) StickerSelectedEvent?.Invoke();
    }

    private void Awake(){
        acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
        cancelButton.onClick.AddListener(delegate { RejectClickedEvent?.Invoke(); });
        undoButton.onClick.AddListener(delegate { UndoEvent?.Invoke(); });
        
        lineToggle.onValueChanged.AddListener(OnLineSelect);
        arrowToggle.onValueChanged.AddListener(OnArrowSelect);
        stickerToggle.onValueChanged.AddListener(OnStickerSelect);
        
        NotifySelectedTool();
    }

    private void OnDestroy(){
        acceptButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
        lineToggle.onValueChanged.RemoveAllListeners();
        arrowToggle.onValueChanged.RemoveAllListeners();
        stickerToggle.onValueChanged.RemoveAllListeners();
    }
}