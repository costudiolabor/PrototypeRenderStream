using System;
using UnityEngine;
using UnityEngine.UI;

public class Tools : View
{
    [SerializeField] private Toggle lineToggle, arrowToggle, stickerToggle;

    [SerializeField] private Button acceptButton, cancelButton, undoButton; // ,screenShot;
    public ColorMenu colorMenu;

    public event Action AcceptClickedEvent, RejectClickedEvent, UndoEvent, LineSelectedEvent, ArrowSelectedEvent, StickerSelectedEvent;

    private void OnEnable()
    {
        acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
        cancelButton.onClick.AddListener(delegate { RejectClickedEvent?.Invoke(); });
        undoButton.onClick.AddListener(delegate { UndoEvent?.Invoke(); });

        lineToggle.onValueChanged.AddListener(LineSelected);
        arrowToggle.onValueChanged.AddListener( ArrowSelected);
        stickerToggle.onValueChanged.AddListener(StickerSelected);
    }

    private void LineSelected(bool isOn)
    {
        if (isOn) return;
        Debug.Log("Line");
        LineSelectedEvent?.Invoke();
    }

    private void ArrowSelected(bool isOn)
    {
        if (isOn) return;
        Debug.Log("Arrow");
        ArrowSelectedEvent?.Invoke();
    }

    private void StickerSelected(bool isOn)
    {
        if (isOn) return;
        Debug.Log("Sticker");
        StickerSelectedEvent?.Invoke();
    }


    private void OnDisable()
    {
        acceptButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();
        undoButton.onClick.RemoveAllListeners();
        lineToggle.onValueChanged.RemoveAllListeners();
        arrowToggle.onValueChanged.RemoveAllListeners();
        stickerToggle.onValueChanged.RemoveAllListeners();
    }

    public void NotifySelected()
    {
        // if(a)            
    }
}