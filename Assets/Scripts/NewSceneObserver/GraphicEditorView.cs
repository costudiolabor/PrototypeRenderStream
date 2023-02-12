using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class GraphicEditorView : ImageEditorView
{
    [SerializeField] private Toggle stickerToggle;
    public PopupDiscriptionSticker popupDiscriptionSticker;

    public event Action StickerSelectedEvent;

    public override void Awake() {
        base.Awake();
        stickerToggle.onValueChanged.AddListener(OnStickerSelect);
        NotifySelectedTool();
    }

    public override void NotifySelectedTool() {
        base.NotifySelectedTool();
        if (stickerToggle.isOn) StickerSelectedEvent?.Invoke();
    }

    private void OnStickerSelect(bool isOn) {
        if (!isOn) return;
        StickerSelectedEvent?.Invoke();
    }
    private void OnDestroy() {
        stickerToggle.onValueChanged.RemoveAllListeners();
    }
}